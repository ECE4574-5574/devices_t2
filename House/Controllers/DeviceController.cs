/**
 * Class which handles REST calls for updating devices
 */
using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using api;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace House
{
public class DeviceController : ApiController
{
	/**
	 * If GET is called with no ID, return a list of all devices in the house.
	 */
	public List<Device> Get()
	{
		if(DeviceModel.Instance.Devices.Count == 0)
		{
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}
		foreach(Device dev in DeviceModel.Instance.Devices)
		{
			dev.LastUpdate = dev.Frame.now();
		}

		return DeviceModel.Instance.Devices;
	}

	/**
	 * If a device is called with an ID, return that device
	 */
	public Device Get(int id)
	{
		Device result = null;
		foreach(Device dev in DeviceModel.Instance.Devices)
		{
			if(dev.ID.DeviceID == (ulong)id)
			{
				dev.LastUpdate = dev.Frame.now();
				result = dev;
				break;
			}
		}

		if(result == null)
		{
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}
		return result;
	}
		
	[HttpPost]
	public async Task UpdateDevice(int id)
	{
		string data = await Request.Content.ReadAsStringAsync();

		if(data.Length == 0)
		{
			throw new HttpResponseException(HttpStatusCode.BadRequest);
		}

		Device result = null;
		foreach(Device dev in DeviceModel.Instance.Devices)
		{
			if(dev.ID.DeviceID == (ulong)id)
			{
				result = dev;
				break;
			}
		}

		if(result == null)
		{
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		var props = result.GetType().GetProperties();
		var json_obj = JObject.Parse(data);
		foreach(var info in props)
		{
			if(info.CanWrite && info.Name != "DeviceID" && info.Name != "Frame")
			{
				JToken field;
				if(!json_obj.TryGetValue(info.Name, StringComparison.OrdinalIgnoreCase, out field))
				{
					continue;
				}
				var value = field.ToObject(info.PropertyType);
				info.SetValue(result, value);
			}
		}
	}
}

}
