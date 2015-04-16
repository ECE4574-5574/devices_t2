/*
* Contributor: Pedro Sorto, Kara Dodenhoff, Steven Cho, Danny Mota, Aakruthi Gopisetty, Dong Nan
* RESTful device API
* Json string will be read through the inputstream
* Json implementation will be handled in the devices class
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using api;
using NDesk.Options;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;
using Hats.Time;
using Newtonsoft.Json.Linq;
using Hats.SimWeather;

namespace House
{
public class ThreadSafeDevice
{
	public ThreadSafeDevice()
	{
		dev = null;
		Mut = new Mutex();
	}
	public Device dev
	{
		get;
		set;
	}

	public Mutex Mut
	{
		get;
		set;
	}
}

public class HouseMain
{
	static private HttpListener _listener;
	static private bool _running;
	static private bool _responding;
	static private bool _is_sim;
	static List<Task> _tasks;
	static private TimeFrame _time;
	static private List<ThreadSafeDevice> _bare_devices;
	static private Dictionary<UInt64, Dictionary<UInt64, ThreadSafeDevice>> _devices;
	static UInt64 _house_id;
	static private int _port;
	static private LinearWeather _weather;
	public static int Main(String[] args)
	{
		bool show_help = false;
		_running = false;
		_responding = false;
		_is_sim = true;
		_bare_devices = new List<ThreadSafeDevice>();
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

        if(show_help)
        {
            PrintHelp(optargs);
			return 1;
        }

		bool success = ParseConfig(config);
		System.Diagnostics.Debug.Assert(success);

        if(_is_sim)
        {
            Console.WriteLine("OK");
        }

		InitListener(_port);

		var input = Console.ReadLine();

		while(!input.Equals("q", StringComparison.OrdinalIgnoreCase))
		{
			if(_time != null)
			{
				_time = JsonConvert.DeserializeObject<TimeFrame>(input);
				foreach(ThreadSafeDevice tsd in _bare_devices)
				{
					tsd.dev.Frame = _time;
				}
				_responding = true;
			}
			input = Console.ReadLine();
		}
		_running = false;
        return 0;
	}

	static bool ParseConfig(Dictionary<string, string> config)
	{
		const string IDKey = "house_id";
		const string ScenarioKey = "test_scenario";

		if(!config.ContainsKey(IDKey) || !config.ContainsKey(ScenarioKey))
		{
			return false;
		}
		string house_id = config[IDKey];
		string scenario = config[ScenarioKey];

		_is_sim = GenerateSimulatedHouse(house_id, scenario);

		return _is_sim;
	}

	static void InitListener(int port)
	{
		String baseURL = String.Format("http://+:{0}/", port.ToString());
		_listener = new HttpListener();
		_listener.Prefixes.Add(baseURL + "device/");
		_listener.Prefixes.Add(baseURL + "house/");
		_listener.Start();

		ProcessHttp(_listener).ContinueWith(async task =>
		{
			await Task.WhenAll(_tasks.ToArray());
		});
	}

	static async Task ProcessHttp(HttpListener listener)
	{
		while(_running)
		{
			var context = await listener.GetContextAsync();
			HandleContextAsync(context);
		}
	}

	static async Task HandleContextAsync(HttpListenerContext ctx)
	{
		if(!_responding)
		{
			ctx.Response.Abort();
			return;
		}
		HttpListenerRequest req = ctx.Request;
		HttpListenerResponse resp = ctx.Response;

		resp.StatusCode = (int)System.Net.HttpStatusCode.NotFound;

		String blob = "";

		switch(req.HttpMethod)
		{
		case "GET":
			UInt64 house = 0;
			UInt64 room = 0;
			UInt64 device = 0;
			if(CheckDeviceURL(req.RawUrl, ref house, ref room, ref device))
			{
				blob = GetDeviceState(house, room, device);
				resp.StatusCode = (int)System.Net.HttpStatusCode.OK;
			}
			break;
		case "POST":
			break;
		}

		if(blob.Length > 0)
		{
			byte[] buff = Encoding.UTF8.GetBytes(blob);
			resp.ContentLength64 = buff.Length;
			resp.OutputStream.Write(buff, 0, buff.Length);
		}
		resp.OutputStream.Close();
	}

	static bool CheckDeviceURL(String rawUrl, ref UInt64 house, ref UInt64 room, ref UInt64 device)
	{
		Regex rgx = new Regex(@"/device/(?<house>\d+)/(?<room>\d+)/(?<device>\d+)");
		Match mc = rgx.Match(rawUrl);

		if(!mc.Success)
		{
			return false;
		}

		GroupCollection group = mc.Groups;
		house = UInt64.Parse(group["house"].Value);
		room = UInt64.Parse(group["room"].Value);
		device = UInt64.Parse(group["device"].Value);

		return true;
	}

	static bool GenerateSimulatedHouse(string house_id, string scenario)
	{
		if(String.IsNullOrEmpty(house_id) || String.IsNullOrEmpty(scenario))
		{
			return false;
		}

		JObject info = JObject.Parse(scenario);
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
			JToken name;

			//found our house
			if(house_obj.TryGetValue("name", out name) &&
				name.ToString() == house_id)
			{
				JToken port_tok;
				JToken dev_tok;
				if(house_obj.TryGetValue("port", out port_tok))
				{
					_port = JsonConvert.DeserializeObject<int>(port_tok.ToString());
				}
				bool success = house_obj.TryGetValue("devices", out dev_tok);
				System.Diagnostics.Debug.Assert(success);
				IJEnumerable<JToken> devices = dev_tok.Children();
				foreach(JToken dev in devices)
				{
					//TODO: Create DeviceInput and DeviceOutput for control
					Device device = Interfaces.DeserializeDevice(dev.ToString(), null, null, null);

					if(device != null)
					{
						ThreadSafeDevice tsd = new ThreadSafeDevice()
						{
							dev = device
						};
						_bare_devices.Add(tsd);
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

				System.Diagnostics.Debug.Assert(_bare_devices.Count > 0);
				status = true;
				break;
			}
		}

		return status;
	}

	static String GetDeviceState(UInt64 house, UInt64 room, UInt64 device)
	{
		return "";
	}

    public static void PrintHelp(OptionSet p)
    {
        Console.WriteLine("Usage: House [Options]");
        Console.WriteLine("Provides the House Interface for a collection of devices.");
        Console.WriteLine();
        Console.WriteLine("Options:");
        p.WriteOptionDescriptions(Console.Out);
    }
}
}