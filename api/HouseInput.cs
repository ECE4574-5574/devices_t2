/**
 * Reading device state from the house app.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hats.Time;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api
{
/**
 * Class which reads state from the house app.
 */
public class HouseInput : IDeviceInput
{
	public HouseInput(string house_info, string device_info)
	{
	
		_houseInfo = house_info;
		_deviceInfo = device_info;
		_Http_Client = new HttpClient();

		try
		{
			var house_addr = JObject.Parse(house_info);
			var temp_url = house_addr.GetValue("house_url").ToObject<string>();
			Debug.WriteLine(temp_url);
			_Http_Client.Timeout = TimeSpan.FromSeconds(10);
			_Http_Client.BaseAddress = new Uri(temp_url);
		}
		catch(JsonException exception)
		{
			throw new ArgumentException(exception.Message);
		}

	}

	public bool read(Device dev)
	{

		return read_driver(dev);
	}

	protected bool read_driver(Device dev)
	{	

		var response = false;
		try {
		UInt64 devID = 0;
			try
			{
				var dev_info = JObject.Parse(_deviceInfo);
				devID = dev_info.GetValue("ID").ToObject<UInt64>();
			}
			catch(JsonException ex)
			{
				return false;
			}

			var query = _Http_Client.GetAsync(String.Format("api/device/{0}", devID));
		
			query.Wait();

			result = query.Result.IsSuccessStatusCode;
		}
		catch(Exception ex)
		{
			Debug.WriteLine("HouseInput failed: " + ex.Message);
			response = false;
		}

		return response;

	}

	public string getURL()
	{
		return _URL;
	}


	protected string _URL;
	protected bool result;
	protected Exception _URLException;
	protected Exception _StreamException;
	protected string _houseInfo;
	protected string _deviceInfo;
	protected HttpClient _Http_Client;
	protected Exception _RequestException;
}

}
