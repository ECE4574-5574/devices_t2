using System;
using System.Net.Http;
using System.Collections.Generic;
using api;

namespace api
{
public class Interfaces
{
	protected HttpClient _http;
	protected Uri _server;

	public Interfaces(Uri serverAddress)
	{
		_http = new HttpClient();
		_server = serverAddress;
	}

	Device createDevice(Uri address, string name, string type, UInt64 house_id, UInt64 room_id = 0)
	{
		//TODO: Post to Server API to request the device be recorded, and get the device.
		var device = (Device)Activator.CreateInstance(type);
		return null;
	}

	/**
	 * Function to get a list of devices from the server, given parameters
	 */
	List<Device> getDevices(UInt64 houseID, UInt64 RoomID = null)
	{
		if(!RoomID)
		{
			//TODO: Query all devices in the house
		}
		else
		{
			//TODO: Query all devices in a given room.
		}	
	}
}
}

