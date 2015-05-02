/**
 * Class which handles REST calls for updating devices
 */
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using api;
using api.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace House
{
public class DeviceController : ApiController
{
	/**
	 * If GET is called with no ID, return a list of all devices in the house.
	 */
	public HttpResponseMessage Get()
	{
		if(DeviceModel.Instance.Devices.Count == 0)
		{
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}
		foreach(Device dev in DeviceModel.Instance.Devices)
		{
			dev.LastUpdate = dev.Frame.now();
		}

		var resp = new HttpResponseMessage();
		resp.StatusCode = HttpStatusCode.OK;
		var json = JsonConvert.SerializeObject(DeviceModel.Instance.Devices, Formatting.None, new DeviceIDOnlyConverter());
		resp.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

		return resp;
	}

	/**
	 * If a device is called with an ID, return that device
	 */
	public HttpResponseMessage Get(UInt64 id)
	{
		Device result = null;
		foreach(Device dev in DeviceModel.Instance.Devices)
		{
			if(dev.ID.DeviceID == id)
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

		var resp = new HttpResponseMessage();
		resp.StatusCode = HttpStatusCode.OK;
		var json = JsonConvert.SerializeObject(result, Formatting.None, new DeviceIDOnlyConverter());
		resp.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
		return resp;
	}
		
	[HttpPost]
	public async Task UpdateDevice(UInt64 id)
	{
		string data = await Request.Content.ReadAsStringAsync();

		if(String.IsNullOrEmpty(data))
		{
			throw new HttpResponseException(HttpStatusCode.BadRequest);
		}

		Device result = null;
		foreach(Device dev in DeviceModel.Instance.Devices)
		{
			if(dev.ID.DeviceID == id)
			{
				result = dev;
				break;
			}
		}

		if(result == null)
		{
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		if(!Interfaces.UpdateDevice(result, data))
		{
			throw new HttpResponseException(HttpStatusCode.BadGateway);
		}
	}
}

}
