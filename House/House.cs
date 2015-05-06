/*
* Contributor: Pedro Sorto, Kara Dodenhoff, Steven Cho, Danny Mota, Aakruthi Gopisetty, Dong Nan
* RESTful device API
* Json string will be read through the inputstream
* Json implementation will be handled in the devices class
*/
using System;
using System.Collections.Generic;
using api;
using Hats.SimWeather;
using Hats.Time;
using Microsoft.Owin.Hosting;
using NDesk.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace House
{

public class HouseMain
{
	static private bool _is_sim;
	static private TimeFrame _time;
	static private int _port;
	static private LinearWeather _weather;
	static private IDisposable _listener;
	public static int Main(String[] args)
	{
		bool show_help = false;
		_is_sim = true;
		_port = 8080;
		_time = null;
		Dictionary<string, string> config = new Dictionary<string, string>();
		var optargs = new OptionSet()
		{
			{
				"t|test_scenario=",
				"JSON blob representing the entire test scenario to run",
				v => config.Add("test_scenario", v)
			},
            {
				"i|house_id=",
				"Unique identifier for the house to simulate from the scenario",
				v => config.Add("house_id", v)
			},
			{
				"p|port=",
				"Port to listen for calls on.",
				v => config.Add("port", v)
			},
			{
				"f|frame=",
				"Time Frame JSON blob to immediately parse",
				v => config.Add("time", v)
			},
            {
				"h|help",
				"Display the help message",
				v => show_help = v != null
			},
        };

        try
        {
            optargs.Parse(args);
        }
        catch(OptionException e)
        {
            Console.WriteLine(e.Message);
            return 1;
        }

		if(show_help || optargs.Count == 0)
        {
            PrintHelp(optargs);
			return 1;
        }

		bool success = ParseConfig(config);
		System.Diagnostics.Debug.Assert(success);

		if(!success)
		{
			Console.WriteLine("Parsing configuration failed.");
			return -1;
		}
        if(_is_sim)
        {
            Console.WriteLine("OK");
        }

		InitListener(_port);

		//Wait until we read 
		while(_time == null)
		{
			var input = Console.ReadLine();
			UpdateTime(input);
		}

		while(true)
		{
			if(Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				if(key.Key == ConsoleKey.Q) //bail bail bail
				{
					break;
				}
			}

			//This gives simulated thermostats a chance to update, or other simulation work to happen
			foreach(Device dev in DeviceModel.Instance.Devices)
			{
				dev.update();
			}

			Thread.Sleep(100);
		}
        return 0;
	}

	static bool ParseConfig(Dictionary<string, string> config)
	{
		const string IDKey = "house_id";
		const string ScenarioKey = "test_scenario";
		const string TimeFrameKey = "time";

		if(!config.ContainsKey(IDKey) || !config.ContainsKey(ScenarioKey))
		{
			return false;
		}
		string house_id = config[IDKey];
		string scenario = config[ScenarioKey];

		_is_sim = GenerateSimulatedHouse(house_id, scenario);

		if(!_is_sim) //we never made it to the real world
		{
			return false;
		}
		if(config.ContainsKey(TimeFrameKey))
		{
			UpdateTime(config[TimeFrameKey]);
		}

		return _is_sim;
	}

	static void InitListener(int port)
	{
		String baseURL = String.Format("http://+:{0}/", port.ToString());
		_listener = WebApp.Start<HouseStartup>(url: baseURL);
	}

	static bool GenerateSimulatedHouse(string house_id, string scenario)
	{
		if(String.IsNullOrEmpty(house_id) || String.IsNullOrEmpty(scenario))
		{
			return false;
		}

		JObject info = null;
		try
		{
			info = JObject.Parse(scenario);
		}
		catch(JsonException ex)
		{
			var error = String.Format("Scenario parsing error: {0}", ex.Message);
			Console.WriteLine(error);
			return false;
		}
		JToken house_list;

		if(!info.TryGetValue("houses", out house_list))
		{
			return false;
		}

		bool status = false;
		IJEnumerable<JToken> houses = house_list.Children();

		//search through houses. Pity this isn't a map.
		foreach(JToken house in houses)
		{
			JObject house_obj = JObject.Parse(house.ToString());
			JToken id_tok;

			//found our house
			if(house_obj.TryGetValue("id", out id_tok) &&
				id_tok.ToString() == house_id)
			{
				JToken port_tok;
				JToken dev_tok;
				if(house_obj.TryGetValue("port", out port_tok))
				{
					_port = JsonConvert.DeserializeObject<int>(port_tok.ToString());
					//must get a valid port value
					if(_port > System.Net.IPEndPoint.MaxPort || _port < System.Net.IPEndPoint.MinPort)
					{
						return false;
					}
				}
				bool success = house_obj.TryGetValue("devices", out dev_tok);
				System.Diagnostics.Debug.Assert(success);
				IJEnumerable<JToken> devices = dev_tok.Children();
				UInt64 id = 0;
				foreach(JToken dev in devices)
				{
					//TODO: Create DeviceInput and DeviceOutput for control
					Device device = Interfaces.DeserializeDevice(dev.ToString(), null, null, new TimeFrame());

					if(device != null)
					{
						device.ID.DeviceID = id++;
						DeviceModel.Instance.Devices.Add(device);
					}
				}

				JToken weather_tok;
				success = house_obj.TryGetValue("weather", out weather_tok);
				System.Diagnostics.Debug.Assert(success);
				_weather = new LinearWeather();
				IJEnumerable<JToken> temps = weather_tok.Children();
				foreach(JToken temp in temps)
				{
					_weather.Add(JsonConvert.DeserializeObject<TemperatureSetPoint>(temp.ToString()));
				}

				System.Diagnostics.Debug.Assert(DeviceModel.Instance.Devices.Count > 0);
				status = true;
				break;
			}
		}

		return status;
	}

	static void UpdateTime(string input)
	{
		if(_time != null)
		{
			return;
		}

		try
		{
			_time = JsonConvert.DeserializeObject<TimeFrame>(input);
			foreach(Device dev in DeviceModel.Instance.Devices)
			{
				dev.Frame = _time;
				var thermo = dev as Thermostat;
				if(thermo != null)
				{
					var simio = new SimTempInput(_weather);
					thermo.resetIO(simio, simio);
				}
			}
			_weather.Frame = _time;
			DeviceModel.Instance.Responding = true;
		}
		catch(JsonException ex)
		{
			_time = null;
		}
	}

	/**
	 * Helper function to print out usage help.
	 */
    private static void PrintHelp(OptionSet p)
    {
        Console.WriteLine("Usage: House [Options]");
        Console.WriteLine("Provides the House Interface for a collection of devices.");
        Console.WriteLine();
        Console.WriteLine("Options:");
        p.WriteOptionDescriptions(Console.Out);
    }
}
}