using System;
using System.Net.Http;
using System.Collections.Generic;
using api;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

	/**
	 * Registers a device with the server, in essence creating it for use in HATS.
	 * \param[in] address Address to locate the device
	 * \param[in] name User friendly name to use for the device.
	 * \param[in] type Type name of the device. Should map to a class name in Devices
	 * \param[in] house_id ID of the house this device is in.
	 * \param[in room_id ID of the room this device is in, if set.
	 */
	Device registerDevice(Uri address, string name, string type, UInt64 house_id, UInt64 room_id = 0)
	{
		//TODO: Post to Server API to request the device be recorded, and get the device.
		var device = (Device)Activator.CreateInstance(Type.GetType(type));
		return device;
	}

	/**
	 * Function to get a list of devices from the server, given parameters
	 */
	List<Device> getDevices(UInt64 houseID)
	{
		var devices = new List<Device>();
		//TODO: Query all devices in a given house.
		return devices;
	}

	/**
	 * Function to get a List of devices from the server, given house & room.
	 */
	List<Device> getDevices(UInt64 houseID, UInt64 roomID)
	{
		var devices = new List<Device>();
		//TODO: Query all devices in a given room.
		return devices;
	}
}
}

