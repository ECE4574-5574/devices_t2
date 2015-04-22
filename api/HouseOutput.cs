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

namespace api
{

/**
 * Class for writing device state to the house.
 */
public class HouseOutput : IDeviceOutput
{
	public HouseOutput(string URL)
	{
		_URL = URL;
	}

	public bool write(Device dev)
	{
		string json = JsonConvert.SerializeObject(dev);
		_json = json;
		var _ = writeHelper(json);

		return true;
	}

	private async Task<string> writeHelper(string json)
	{
		try
		{
			var request = (HttpWebRequest)WebRequest.Create(_URL);

			var data = Encoding.UTF8.GetBytes(json);
			_data = data;

			request.Method = "POST";
			request.ContentType = "application/json";

			try {
				using (var stream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
				{
					await stream.WriteAsync(data, 0, data.Length);
				}				
			} catch (Exception ex) {
				_StreamException = ex;
				return null;
			}

			try {

				WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);

				var responseString = responseObject.GetResponseStream();
				var sr = new StreamReader(responseString);
				string received = await sr.ReadToEndAsync();

				return received;
				
			} catch (Exception ex) {
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


	public string getURL()
	{
		return _URL;
	}

	public byte[] getData()
	{
		return _data;
	}

	public string getJSON()
	{
		return _json;
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

	protected string _URL;
	protected string _json;
	protected byte[] _data;
	protected Exception _URLException;
	protected Exception _StreamException;
	protected Exception _RequestException;
}

}
