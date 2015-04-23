/**
 * Reading device state from the house app.
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
 * Class which reads state from the house app.
 */
public class HouseInput : IDeviceInput
{
	public HouseInput(string house_info, string device_info)
	{
	}

	public bool read(Device dev)
	{
		var _ = readHelper(dev);

		return true;
	}

	private async Task<string> readHelper(Device dev)
	{
		try
		{
			var request = (HttpWebRequest)WebRequest.Create(_URL);
			request.Method = "GET";
			request.ContentType = "application/json";

			try
			{
				using(var stream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
				{
					result = new byte[stream.Length];
					await stream.ReadAsync(result, 0, (int)stream.Length);
				}

				Interfaces.UpdateDevice(dev, result.ToString());
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
				var streamread = new StreamReader(responseString);
				string confirm = await streamread.ReadToEndAsync();
				return confirm;

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

	public string getURL()
	{
		return _URL;
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
	protected byte[] result;
	protected Exception _URLException;
	protected Exception _StreamException;
	protected Exception _RequestException;
}

}
