	/**
 * Input for reading device state from the Server API
 */
using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using PortableRest;
using Newtonsoft.Json;


namespace api
{
public class ServerInput : IDeviceInput
{
	string _server_uri;

	public ServerInput(string server_uri) 
	{
		_server_uri= server_uri;
	}

	/**
	 * Updates device from the server
	 */
	public bool read(Device dev)
	{ 
		bool server_input_status = false; //The variable tracks the successful updataion of state for the app and decision making


		//this try block tries to get a deserialized string of the state from the server
		try
		{
		var client = new RestClient();
		client.BaseUrl = _server_uri;
		var request = new RestRequest("api/storage/device/{house}/{space}/{device}");//the default request is GET
		request.AddUrlSegment("house", dev.ID.HouseID.ToString());
		request.AddUrlSegment("space", dev.ID.HouseID.ToString());
		request.AddUrlSegment("device", dev.ID.HouseID.ToString());
		var response = client.ExecuteAsync<string>(request);//tries to get a deserialized string of device state as the response.
		 
			if(response.Status ==TaskStatus.Created)//condition check for a successful completion of the GET request
			{	
			bool update_status = Interfaces.UpdateDevice(dev, response.Result);// updating the device state obtained from server
															//the assumption is that response.Result has the deserialized device state
			if(update_status == true)
				{
				server_input_status = true;
			
				}
			}
			else 
			{
				Debug.WriteLine("Trouble querying the server"+response.Status);
			}
		return server_input_status;
		}
		catch (Exception e)
		{
			Debug.WriteLine("Server Input Error: " + e.Message);
			Debug.WriteLine("Server Input Error: " + e.InnerException.Message);
		}
		return server_input_status;
		//return response;
		//var blob = JsonConvert.DeserializeObject(dev);
		//RestResponse response; //= client.ExecuteAsync(request);
		//RestResponse response = client.ExecuteAsync(request);
		//Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
		//Device dev1= response.Result;
		//dev = dev1;
		//var content = response.Result;

		//Device DeserializeDevice(string info, IDeviceInput inp, IDeviceOutput outp, TimeFrame frame)
		//POST api/house/device/state
		//return true;
	}
}

}
