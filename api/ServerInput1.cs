/**
  * Input for reading device state from the Server API
*/
using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using PortableRest;//This class cannot use RestSharp as they are configured to be compatible with PCL libraries
using Newtonsoft.Json;


namespace api
{
public class ServerInput1
{
	//string _server_uri;

	//public ServerInput1(string server_uri) 
	//{
	//	_server_uri= server_uri;
	//}

	/**
	 * Updates device from the server
	 */
	public async Task<HttpStatusCode> read()
	{ 
		//return true;



		bool server_input_status = false; //The variable tracks the successful updataion of state for the app and decision making


		//this try block tries to get a deserialized string of the state from the server

		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(10);

		//client.BaseAddress = new Uri(ConfigModel.Url);


		var response = await client.GetAsync("http://serverapi1.azurewebsites.net/api/app/device/");

		return response.StatusCode;
		//	return response.StatusCode;
		//else
		//	return false;



		//return false;
		return response.StatusCode;	
	}

	//return server_input_status;
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

//}
}
