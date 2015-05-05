/**
 * Writing device state to the house app.
 * Written by: Brianna Kicia
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
 * Class for writing device state to the house.
 */
public class HouseOutput : IDeviceOutput
{
	public HouseOutput(string house_info, string device_info)
	{
		_houseInfo = house_info;
		_deviceInfo = device_info;
		_client = new HttpClient();

		try
		{
			var house_tok = JObject.Parse(house_info);
			var url = house_tok.GetValue("house_url").ToObject<string>();
			Debug.WriteLine(url);
			_client.BaseAddress = new Uri(url);
		}
		catch(JsonException ex)
		{
			throw new ArgumentException(ex.Message);
		}
	}

	public bool write(Device dev)
	{
		string desiredState = JsonConvert.SerializeObject(dev);

		return writeHelper(desiredState);
	}

	protected bool writeHelper(string json)
	{
		var result = false;
		try
		{
			UInt64 deviceID = 0;
			try
			{
				var dev_tok = JObject.Parse(_deviceInfo);
				deviceID = dev_tok.GetValue("ID").ToObject<UInt64>();
			}
			catch(JsonException ex)
			{
				return false;
			}

			var post = _client.PostAsync(String.Format("api/device/{0}", deviceID),
				new StringContent(json, Encoding.UTF8, "application/json"));
			post.Wait();

			result = post.Result.IsSuccessStatusCode;
		}
		catch(Exception ex)
		{
			Debug.WriteLine("HouseOutput failed: " + ex.Message);
			result = false;
		}

		return result;
	}

	public string getHouseInfo()
	{
		return _houseInfo;
	}

	protected string _houseInfo;
	protected string _deviceInfo;
	protected HttpClient _client;
}

}
