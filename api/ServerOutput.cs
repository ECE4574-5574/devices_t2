/**
 * Input for writing device state to the Server API
 */
using System;

namespace api
{
public class ServerOutput : IDeviceOutput
{
	public ServerOutput()
	{
	}

	public bool write(Device dev)
	{
		string houseid = dev.id.houseid.ToString();
	        string roomid = dev.id.roomid.ToString();
	        string deviceid = dev.id.deviceid.ToString();
	        string url = "serverapi1d.azurewebsites.net";
	        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
	        request.Method = "POST";
	        WebHeaderCollection headers = (request as HttpWebRequest).Headers;
	        headers.Add("api/house/device/" + houseid + "/" + roomid + "/" + deviceid);
		return true;
	}
}
}

