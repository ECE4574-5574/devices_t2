/**
 * Input for reading device state from the Server API
 */
using System;

namespace api
{
public class ServerInput : IDeviceInput
{
	public ServerInput() 
	{
	}

	/**
	 * Updates device from the server
	 */
	public bool read(Device dev)
	{
		//DeviceRepository rep = new DeviceRepository();
	        string houseid = dev.id.houseid.ToString();
	        string roomid = dev.id.roomid.ToString();
	        string deviceid = dev.id.deviceid.ToString();
	        //string json = rep.GetDevice(houseid, roomid, deviceid).ToString();
	        string url = "serverapi1.azurewebsites.net";
	        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
	        request.Method = "GET";
	        WebHeaderCollection headers = (request as HttpWebRequest).Headers;
	        headers.Add("api/house/device/" + houseid + "/" + roomid + "/" + deviceid);
	        HttpWebResponse response;
	        response = request.GetResponse() as HttpWebResponse;
	        Stream stream = response.GetResponseStream();
	        StreamReader readStream = new StreamReader(stream, Encoding.UTF8);
	        string json = readStream.ReadToEnd();
	        return Interfaces.UpdateDevice(dev, json);
		//TODO: Implement this function
		//return true;
	}
}

}
