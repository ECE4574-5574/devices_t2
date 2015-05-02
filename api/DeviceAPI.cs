using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Hats.Time;

namespace api
{

public class Interfaces
{
	protected HttpClient _http;
	protected Uri _server;

	public Interfaces(Uri serverAddress)
	{
		_http = new HttpClient();
		_server = serverAddress;
	}
	public Interfaces()
	{
		_http = new HttpClient();
		_server = new Uri ("http://serverapi1.azurewebsites.net");
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
		string houseID = house_id.ToString();
		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(50);
		client.BaseAddress = new Uri ("http://serverapi1.azurewebsites.net");

		var response = client.GetAsync("api/app/device/enumeratedevices/" + houseID).Result;

		List<string> listOfDevices= new List<string>();

		if(!response.IsSuccessStatusCode)
		{
			return listOfDevices;
		}

		try
		{
			var content = response.Content.ReadAsStringAsync();
			content.Wait();
			JArray unregisteredDevices = JArray.Parse(content.Result);
			foreach(JToken Device in unregisteredDevices)
			{
				listOfDevices.Add(Device.ToString());
			}
		}
		catch(JsonException ex)
		{
		}
		return listOfDevices;
	
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
		return null;
	}

	/**
	 * Given a device, remove it from the system as a registered device. This should only be done
	 * as a result of a user request, as this deletes all references to the device.
	 * \param[in] Device to remove
	 * \param[out] Flag indicating success
	 */
	public bool deleteDevice(Device dev)
	{
		return true;
	}

	/**
	 * Function to get a list of devices from the server.
	 * \param[in] ID of the House to get devices for
	 * \param[out] List of devices in a given house.
	 */
	public List<Device> getDevices(UInt64 houseID)
	{
		var devices = new List<Device>();
		//TODO: Query all devices in a given house.
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
		var devices = new List<Device>();
		//TODO: Query all devices in a given room.
		return devices;
	}

	/**
	 * Given a JSON string representing a device, instantiates the device as desired.
	 * \param[in] info JSON string representing device. Must have a key named "class" which
	 *            names the class deriving from Device to instantiate.
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
				device = (Device)Activator.CreateInstance(device_type, inp, outp, frame);
				JsonConvert.PopulateObject(info, device);
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
	 * all public properties which are not the DeviceID or TimeFrame to
	 * whatever value is in the JSON blob.
	 * \param[in] dev Device to be updated
	 * \param[in] json JSON blob of fields to update
	 * \param[out] Flag indicating if at least one field was updated
	 */
	public static bool UpdateDevice(Device dev, string json)
	{
		if(dev == null || String.IsNullOrEmpty(json))
		{
			return false;
		}

		bool updated_value = false;
		try
		{
			var props = dev.GetType().GetRuntimeProperties();
			var json_obj = JObject.Parse(json);

			foreach(var info in props)
			{
				if(!info.SetMethod.IsPublic || info.Name == "DeviceID" || info.Name == "Frame")
				{
					continue;
				}
				JToken field;
				if(!json_obj.TryGetValue(info.Name, StringComparison.OrdinalIgnoreCase, out field))
				{
					continue;
				}
				var value = field.ToObject(info.PropertyType);
				info.SetValue(dev, value);
				updated_value = true;
			}
		}
		catch(JsonException ex)
		{
			//TODO: Report error somehow?
		}

		return updated_value;
	}
}

}
