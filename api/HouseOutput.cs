/**
 * Writing device state to the house app.
 * Written by: Brianna Kicia
 */
using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using Hats.Time;

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
	}

	public bool write(Device dev)
	{
		string desiredState = JsonConvert.SerializeObject(dev);
		string houseID = dev.ID.HouseID.ToString();
		string roomID = dev.ID.RoomID.ToString();
		string deviceID = dev.ID.DeviceID.ToString();

		var _ = writeHelper(desiredState, houseID, roomID, deviceID);

		return true;
	}

	async Task<string> writeHelper(string json, string houseID, string roomID, string deviceID)
	{	
		try
		{
			var request = (HttpWebRequest)WebRequest.Create(_houseInfo);

			var data = Encoding.UTF8.GetBytes(json);
			_data = data;

			request.Method = "POST";
			request.ContentType = "application/json";
			request.Headers["header"] = ("api/house/device/" + houseID + "/" + roomID + "/" + deviceID);

			try
			{
				using(var stream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
				{
					await stream.WriteAsync(data, 0, data.Length);
				}				
			}
			catch(Exception ex)
			{
				_StreamException = ex;
				return null;
			}

			try
			{

				WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);

				var responseString = responseObject.GetResponseStream();
				var sr = new StreamReader(responseString);
				string received = await sr.ReadToEndAsync();

				return received;
				
			}
			catch(Exception ex)
			{
				_RequestException = ex;
				return null;
			}
		}
		catch(Exception ex)
		{
			_URLException = ex;
			return null;
		}
	}


	public string getHouseInfo()
	{
		return _houseInfo;
	}

	public byte[] getData()
	{
		return _data;
	}

	public Exception getURLException()
	{
		return _URLException;
	}

	public Exception getStreamException()
	{
		return _StreamException;
	}

	public Exception getRequestException()
	{
		return _RequestException;
	}

	protected string _houseInfo;
	protected string _deviceInfo;
	protected byte[] _data;
	protected Exception _URLException;
	protected Exception _StreamException;
	protected Exception _RequestException;
}

}
