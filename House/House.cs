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
	public static int Main(String[] args)
	{
		bool show_help = false;
		_running = false;
		_responding = false;
		_is_sim = true;
		_bare_devices = new List<ThreadSafeDevice>();
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

		ParseConfig(config);

        if(_is_sim)
        {
            Console.WriteLine("OK");
        }
		else
		{
			_running = true;
		}

		InitListener(_port);

		var input = Console.ReadLine();

		while(!input.Equals("q", StringComparison.OrdinalIgnoreCase))
		{
			try
			{
				_time = JsonConvert.DeserializeObject<TimeFrame>(input);
				_responding = true;
			}
			catch(JsonReaderException ex)
			{
			}
		}
		_running = false;
        return 0;
	}

	static bool ParseConfig(Dictionary<string, string> config)
	{
		string house_id = "";
		string scenario = "";
		_is_sim = config.TryGetValue("house_id", out house_id) && config.TryGetValue("test_scenario", out scenario);
		if(_is_sim)
		{
			_is_sim = GenerateSimulatedHouse(house_id, scenario);
		}
			
		if(!config.ContainsKey("port") || !int.TryParse(config["port"], out _port))
		{
			_port = 8080;
		}

		return true;
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

		IJEnumerable<JToken> houses = house_list.Children();

		foreach(JToken house in houses)
		{
			JObject house_obj = JObject.Parse(house.ToString());
			JToken name;

			if(house_obj.TryGetValue("name", out name) &&
				name.ToString() == house_id)
			{
				JToken dev_tok;
				house_obj.TryGetValue("devices", out dev_tok);
				IJEnumerable<JToken> devices = dev_tok.Children();
				foreach(JToken dev in devices)
				{
					Device device = deserializeDevice(dev.ToString());

					if(device != null)
					{
						ThreadSafeDevice tsd = new ThreadSafeDevice()
						{
							dev = device
						};
						_bare_devices.Add(tsd);
					}
				}
				break;
			}
		}

		return true;
	}

	static Device deserializeDevice(string info)
	{
		JObject device_obj = JObject.Parse(info);
		JToken type_tok;
		if(!device_obj.TryGetValue("class", out type_tok))
		{
			return null;
		}

		var device_type = GetDeviceType("api." + type_tok.ToString());
		Device device = null;
		if(device_type != null)
		{
			device = (Device)JsonConvert.DeserializeObject(info, device_type);
		}
		return device;
	}

	public static Type GetDeviceType(string typeName)
	{
		var type = Type.GetType(typeName);
		if(type != null)
		{
			return type;
		}
		foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
		{
			type = a.GetType(typeName);

			if(type != null)
			{
				return type;
			}
		}

		return null;
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