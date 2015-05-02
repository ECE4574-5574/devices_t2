/**
 * Input for reading device state from the Server API
 */
using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;

namespace api
{
public class ServerInput : IDeviceInput
{

	string _serverURL;
	public ServerInput(string server_URL)
	{
		_serverURL = server_URL;
	}
	public ServerInput()
	{
		_serverURL = "http://serverapi1.azurewebsites.net/";
	}

	public bool read(Device dev)
	{
		//string json = JsonConvert.SerializeObject(dev);

		try
		{
		var response = HttpRequest(dev);
		if(response.StatusCode == HttpStatusCode.OK)
		{
			
			bool update_success = Interfaces.UpdateDevice(dev, response.Content.ToString());
			return true;
		}
		else
		{
			return false;
		}
		}
		catch (Exception e)
		{
			Debug.WriteLine("Server Input Error: " + e.Message);
			Debug.WriteLine("Server Input Error: " + e.InnerException.Message);
			return false;
		}
	}

	public HttpResponseMessage HttpRequest(Device dev1)
	{	

		try
		{
		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(10);
		client.BaseAddress = new Uri(_serverURL);
		string houseID = dev1.ID.HouseID.ToString();
		string roomID = dev1.ID.RoomID.ToString();
		string deviceID = dev1.ID.DeviceID.ToString();
		var response = client.GetAsync("/api/storage/device/" +houseID+ "/"+roomID+"/"+deviceID).Result;
		return response;
		}
		catch (Exception e)
		{
			Debug.WriteLine("Server Input Error: " + e.Message);
			Debug.WriteLine("Server Input Error: " + e.InnerException.Message);
			return null;
		}

	}
}
}
