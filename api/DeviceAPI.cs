using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using api;
using Hats.Time;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using api.Converters;
using Hats.Time;
using System.Linq;


namespace api
{

public class Interfaces
{
	protected Uri _server;
	protected TimeFrame _frame;

	public Interfaces(Uri serverAddress, TimeFrame frame = default(TimeFrame))
	{
		_server = serverAddress;

		if(frame == null)
		{
			frame = new TimeFrame();
		}

		_frame = frame;
	}

	/**
	 * Function which is called to request a list of devices present at a given location, which
	 * are not currently registered in the HATS system. Since these devices are not registered,
	 * no ID has been assigned.
	 * 
	 * \param[in] address Location of the house to query for unregistered devices.
	 * \param[out] List of strings which represent devices which could be registered.
	 */
	public List<string> enumerateDevices(UInt64 house_id)
	{
		if(house_id < 0)
			return null;
		//Post GET call to _server + "/api/app/device/enumeratedevices/{house_id}"
		WebRequest request = WebRequest.Create(_server + "/api/app/device/enumeratedevices/{house_id}");
		request.ContentType = "application/json";
		request.Method = "GET";
		string str;
		try
		{
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				var stream = response.GetResponseStream();
				var reader = new StreamReader(stream);
				str = reader.ReadToEnd();
			}
		}
					
		catch (Exception ex) {
			_StreamException = ex;
			return null;
		}
		//Get result of GET
		//Turn Content into JArray
		JArray jArr =  JArray.Parse(str);
		//JToken jTok;
		List<string> devicelist = new List<string>();
		//Iterate over JArray, for each JToken inside, call List.Add(JToken.ToString());
		//Parse Contents to a list of strings, where the strings are JSON blobs
		foreach(var jTok in jArr)
		{
			devicelist.Add(jTok.ToString());
		}
		//return
		return devicelist;
	}
        	public List<string> enumerateDevices(UInt64 house_id)
	{
		//TODO: Verify the input parameters are sufficient
		//TODO: Implement this function
        if (house_id < 0)
        {
            return null;
        }
        //get device list
		var server = new Interfaces("http://serverapi1.azurewebsites.net");
		// get some device list
		WebRequest request = WebRequest.Create(server._http + "/api/storage/device/" + house_id);
		request.ContentType = "application/json";
		request.Method = "GET";
		JObject jobject = new JObject();
		string json = jobject.ToString();
		try
		{
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				var stream = response.GetResponseStream();
				var reader = new StreamReader(stream);
				json = reader.ReadToEnd();
			}
		}
		catch (WebException we)
		{
			Console.WriteLine("StorageGetDevicesInHouse fails.");
			return null;
		}
        var devicelist = new List<string>();
		foreach(var dev in devicelist)//fake_Devices)
		{
			JObject device_list = JObject.Parse(json);
			JToken type_tok;
			if (!device_list.TryGetValue("houseid", StringComparison.OrdinalIgnoreCase, out type_tok))
			{
				return null;
			}
			var houseID = type_tok.ToUint64();
			if(houseID == house_id)
				devicelist.Add(dev);
		}
        return device;
	}
	}

	/**
	 * Registers a device with the server, in essence creating it for use in HATS.
	 * \param[in] name User friendly name to use for the device.
	 * \param[in] house_id ID of the house this device is in.
	 * \param[in] info Blob from enumerateDevices() which represents the device of interest
	 * \param[in] room_id ID of the room this device is in, if set.
	 * \param[out] Object providing control of the registered device, or null if it fails to register.
	 */
	public Device registerDevice(string name, UInt64 house_id, string info, UInt64 room_id = 0)
	{
		//TODO: Verify parameters here are sufficient
		//TODO: Post to Server API to request the device be recorded, and get the device.
        if (String.IsNullOrEmpty(name) || house_id < 0 || String.IsNullOrEmpty(info))
        {
            return null;
        }
        JObject device_obj = JObject.Parse(info);
        JToken type_tok;
        if (!device_obj.TryGetValue("class", StringComparison.OrdinalIgnoreCase, out type_tok))
        {
            return null;
        }
		var device_type = GetDeviceType(type_tok.ToString());
        var device_name = name;
        Device device = null;
        if (device_name != null)
        {
            device = (Device)Activator.CreateInstance(device_type, house_id, room_id);
			device.Name = device_name;
			JsonConvert.PopulateObject(info, device);
        }
        return device;

		
	}

	/**
	 * Given a device, remove it from the system as a registered device. This should only be done
	 * as a result of a user request, as this deletes all references to the device.
	 * \param[in] Device to remove
	 * \param[out] Flag indicating success
	 */
	public bool deleteDevice(Device dev)
	{
        if (dev == null)
            return false;

        var dlist = new List<Device>();
        //dlist = Get[] device list from server API
        var item = dlist.First(x => x == dev);
        dlist.Remove(item);
		return true;
	}
		
	/**
	 * Function to get a list of devices from the server.
	 * \param[in] ID of the House to get devices for
	 * \param[out] List of devices in a given house.
	 */
	public List<Device> getDevices(UInt64 houseID)
	{
		if (houseID < 0)
		{
			return null;
		}

		// testing
		List<Device> fake_Devices = new List<Device>()
		{
			new AlarmSystem(null, null, null),
			new CeilingFan(null, null, null),
			new GarageDoor(null, null, null),
			new LightSwitch(null, null, null),
			new Thermostat(null, null, null)
		};
		foreach (var device in fake_Devices)//devices)
		{
			device.ID.HouseID = 1;
			device.ID.RoomID = 2;
			device.ID.DeviceID = 3;
		}
		//ending test
		//get device list
		var client = new RestClient("http://127.0.0.1:8081");
		var query = new RestRequest(Method.GET);
		query.Resource = "api/storage/device/{houseid}";
		query.RequestFormat = DataFormat.Json;
		var resp = client.Execute(query);
		var respList = JArray.Parse(resp.Content);
		// get some device list
		var devicelist = new List<Device>();
		devicelist = respList.ToObject<List<Device>>();

		//var address = new Interfaces(address);
		// get some device list
		//var devices = new List<Device>();
		//TODO: Query all devices in a given house.
		var devices = new List<Device>();
		foreach (var device in devicelist)//fake_Devices)
        {
			JToken type_tok;
			if(!dev.TryGetValue("houseid", StringComparison.OrdinalIgnoreCase, out type_tok))
			{
				return null;
			}
			if(type_tok == houseID)
				devices.Add(device);
        }

			
		return devices;
	}

	/**
	 * Function to get a List of devices from the server, given house & room.
	 * \param[in] houseID ID of House to get devices from
	 * \param[in] roomID ID of Room to get devices from. 0 means "Devices not assigned a room"
	 * \param[out] List of Devices matching the requested parameters.
	 */
	public List<Device> getDevices(UInt64 houseID, UInt64 roomID)
	{
		if(houseID < 0)
			return null;
		//get device list
		var client = new RestClient("http://127.0.0.1:8081");
		var query = new RestRequest(Method.GET);
		query.Resource = "api/storage/device/{houseid}";
		query.RequestFormat = DataFormat.Json;
		var resp = client.Execute(query);
		var respList = JArray.Parse(resp.Content);
		// get some device list
		var devicelist = new List<Device>();
		devicelist = respList.ToObject<List<Device>>();

		//var address = new Interfaces(address);
		// get some device list
		//var devices = new List<Device>();
		//TODO: Query all devices in a given house.
		var devices = new List<Device>();
		foreach (var device in devicelist)//fake_Devices)
		{
			JToken type_tok;
			if(!dev.TryGetValue("houseid", StringComparison.OrdinalIgnoreCase, out type_tok))
			{
				return null;
			}
			if(type_tok == houseID && roomID != 0)
				devices.Add(device);
			else if(type_tok == houseID && roomID == 0)
				registerDevice(device.Name, houseID, device.Content);
		}
			
		return devices;
	}

	/**
	 * Given relevant information string, attempts to create a device capable of communicating with a device through the server.
	 * \param[in] info JSON string representing the device, which came from the server
	 * \param[in] frame TimeFrame for timestamping data for this device
	 */
	public Device CreateDevice(string info, TimeFrame frame)
	{
		var inp = new ServerInput(_server.ToString());
		var outp = new ServerOutput(_server.ToString());
		return Interfaces.DeserializeDevice(info, inp, outp, frame);
	}

	/**
	 * Given a JSON string representing a device, instantiates the device as desired. Raw instantiation, which shouldn't
	 * be used by end-clients typically
	 * \param[in] info JSON string representing device. Must have a key named "class" which
	 *            names the class deriving from Device to instantiate.
	 * \param[in] inp IDeviceInput to create device with
	 * \param[in] outp IDeviceOutput to create device with
	 * \param[in] frame TimeFrame to initialize the frame
	 */
	public static Device DeserializeDevice(string info, IDeviceInput inp, IDeviceOutput outp, TimeFrame frame)
	{
		if(String.IsNullOrEmpty(info))
		{
			return null;
		}

		Device device = null;
		try
		{
			JObject device_obj = JObject.Parse(info);
			JToken type_tok;
			if(!device_obj.TryGetValue("class", StringComparison.OrdinalIgnoreCase, out type_tok))
			{
				return null;
			}

			var device_type = GetDeviceType(type_tok.ToString());
			if(device_type != null)
			{
				device = (Device)Activator.CreateInstance(device_type, null, null, frame);
				update(device, info, update_id:true, force:true);

				device.resetIO(inp, outp); //this way, population doesn't trigger house comms
			}
		}
		catch(JsonException ex)
		{
			//TODO: Figure out how to pass exceptions up, or record error
		}
		return device;
	}

	/**
	 * Attempts to get the type of a specific device, given the fully
	 * qualified name.
	 * \param[in] typeName Fully qualified classname of device
	 * \param[out] Type representing device, or NULL if it doesn't exist
	 */
	private static Type GetDeviceType(string typeName)
	{
		return Type.GetType("api." + typeName);
	}

	/**
	 * Given a device and the JSON string to update it with, this will update
	 * all public properties which are not the TimeFrame to
	 * whatever value is in the JSON blob.
	 * \param[in] dev Device to be updated
	 * \param[in] json JSON blob of fields to update
	 * \param[in] silence_io Temporarily disable the Device IO for simple value updating
	 * \param[in] update_id Flag indicating if the DeviceID should be updated at all
	 * \param[in] force Flag indicating if public/private flags should be respected for the update
	 * \param[out] Flag indicating if at least one field was updated
	 */
	public static bool UpdateDevice(Device dev, string json, bool silence_io = false,
		bool update_id = false)
	{
		return update(dev, json, silence_io, update_id, force: false);
	}

	/**
	 * Given an instance of the device, attempts to update all public properties with info
	 * from the new device instance. Note that devices currently must have the same ID, but
	 * the actual classes do not need to be the same.
	 * \param[in] old_dev Device which will be updated.
	 * \param[in] new_dev Device which contains values to be updated with
	 * \param[in] silence_io Temporarily disable the Device IO for simple value updating
	 * \param[out] Flag indicating if a field was updated with a new value
	 */
	public static bool UpdateDevice(Device old_dev, Device new_dev, bool silence_io = false)
	{
		if(old_dev == null || new_dev == null)
		{
			return false;
		}

		if(old_dev.ID != new_dev.ID)
		{
			return false;
		}
			
		var old_props = old_dev.GetType().GetRuntimeProperties();
		var new_props = new_dev.GetType().GetRuntimeProperties();
		bool update_field = false;


		IDeviceInput inp = null;
		IDeviceOutput outp = null;

		if(silence_io)
		{
			inp = old_dev.Input;
			outp = old_dev.Output;
		}
		foreach(var old_info in old_props)
		{
			//Can't update this method
			if(old_info.SetMethod == null || !old_info.SetMethod.IsPublic)
			{
				continue;
			}
			foreach(var new_info in new_props)
			{
				if(new_info.GetMethod == null || !new_info.GetMethod.IsPublic)
				{
					continue;
				}

				if(new_info.Name == old_info.Name)
				{
					update_field |= old_info.GetValue(old_dev) != new_info.GetValue(new_info);
					old_info.SetValue(old_dev, new_info.GetValue(new_dev));
					break;
				}
			}
		}

		if(silence_io)
		{
			old_dev.resetIO(inp, outp);
		}
		return update_field;
	}

	/**
	 * Internal device updating function.
	 * \param[in] dev Device to be updated
	 * \param[in] json JSON blob of fields to update
	 * \param[in] silence_io Temporarily disable the Device IO for simple value updating
	 * \param[in] update_id Flag indicating if the DeviceID should be updated at all
	 * \param[in] force Flag indicating if public/private flags should be respected for the update
	 * \param[out] Flag indicating if at least one field was updated
	 */
	protected static bool update(Device dev, string json, bool silence_io = false,
		bool update_id = false, bool force = false)
	{
		if(dev == null)
		{
			return false;
		}

		IDeviceInput inp = null;
		IDeviceOutput outp = null;
		if(silence_io)
		{
			inp = dev.Input;
			outp = dev.Output;
			dev.resetIO();
		}
		bool updated_value = true;
		try
		{
			var props = dev.GetType().GetRuntimeProperties();
			var json_obj = JObject.Parse(json);

			foreach(var info in props)
			{
				if(info.SetMethod == null || !(force || info.SetMethod.IsPublic) || info.Name == "Frame")
				{
					continue;
				}
				JToken field;
				if(!json_obj.TryGetValue(info.Name, StringComparison.OrdinalIgnoreCase, out field))
				{
					continue;
				}

				if(update_id && info.Name == "ID")
				{
					if(field.Type == JTokenType.Integer)
					{
						dev.ID.DeviceID = field.ToObject<UInt64>();
					}
					else
					{
						dev.ID = field.ToObject<FullID>();
					}
				}
				else if(info.Name != "ID")
				{
					var value = field.ToObject(info.PropertyType);
					info.SetValue(dev, value);
				}
				updated_value = true;
			}
		}
		catch(JsonException ex)
		{
			//TODO: Report error somehow?
		}

		if(silence_io)
		{
			dev.resetIO(inp, outp);
		}
		return updated_value;
	}
}

}
