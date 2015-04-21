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
		var _ = writeHelper(json);

		//Console.WriteLine("testing");

		return true;
	}

	public async Task<bool> writeHelper(string json)
	{
		var request = (HttpWebRequest)WebRequest.Create(_URL);

		var data = Encoding.UTF8.GetBytes(json);

		request.Method = "POST";
		//request.ContentType = "HouseAPI";
		//request.ContentLength = data.Length;

		using (var stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null))
		{
			stream.Write(data, 0, data.Length);
		}
			
		using(var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
		{
			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
		}
		return true;
	}

	protected string _URL;
}

}
