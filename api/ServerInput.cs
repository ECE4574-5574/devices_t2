/**
 * Input for reading device state from the Server API
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api
{

public class ServerInput : IDeviceInput
{
	protected string _serverURL;
	protected HttpClient _client;
	public ServerInput(string server_URL)
	{
		_client = new HttpClient();
		_client.BaseAddress = new Uri(server_URL);
		_serverURL = server_URL;
	}

	public bool read(Device dev)
	{
		if(dev == null)
		{
			return false;
		}
		var result = false;
		try
		{
			HttpResponseMessage response = GetResponse(dev);
			if(response == null)
			{
				return result;
			}
			result = response.IsSuccessStatusCode;
			if(result)
			{
				var content = response.Content.ReadAsStringAsync();
				content.Wait();
				result = Interfaces.UpdateDevice(dev, content.Result, silence_io:false, update_id:false);
			}
		}
		catch (Exception e)
		{
			Debug.WriteLine("Server Input Error: " + e.Message);
		}

		return result;
	}

	public HttpResponseMessage GetResponse(Device dev1)
	{
		HttpResponseMessage resp = null;
		try
		{
			var resource_address = String.Format("api/storage/device/{0}/{1}/{2}", dev1.ID.HouseID, dev1.ID.RoomID,
				dev1.ID.DeviceID);
			var rr = _client.GetAsync(resource_address);
			rr.Wait();
			resp = rr.Result;
		}
		catch (Exception e)
		{
			Debug.WriteLine("GetResponse Error: " + e.Message);
		}

		return resp;
	}
}
}