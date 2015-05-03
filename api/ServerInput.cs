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
	string _serverURL;
	public ServerInput(string server_URL)
	{
		_serverURL = server_URL;
	}

	public bool read(Device dev)
	{
		var result = false;
		try
		{
			var response = GetResponse(dev);
			if(response == null)
			{
				return result;
			}
			result = response.IsSuccessStatusCode;
			if(result)
			{
				var content = response.Content.ReadAsStringAsync();
				content.Wait();
				result = Interfaces.UpdateDevice(dev, content.Result);
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
			var client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(10);
			client.BaseAddress = new Uri(_serverURL);
			var resource_address = String.Format("/api/storage/device/{0}/{1}/{2}", dev1.ID.HouseID, dev1.ID.RoomID,
				dev1.ID.DeviceID);
			resp = client.GetAsync(resource_address).Result;
		}
		catch (Exception e)
		{
			Debug.WriteLine("Server Input Error: " + e.Message);
		}

		return resp;
	}
}
}