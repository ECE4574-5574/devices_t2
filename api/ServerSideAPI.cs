using System;
using System.Collections.Generic;
using Hats.Time;

namespace api
{
public class ServerSideAPI
{
	public ServerSideAPI()
	{
	}

	/**
	 * Function which is called to request a list of devices present in a house.
	 * \param[in] house_url URL for the house to query
	 * \param[out] List of strings representing all devices in the house. Each string should uniquely identify a single
	 *             device in the house.
	 */
	public static List<string> enumerateDevices(string house_url)
	{
		var list = new List<string>();
		return list;
	}

	/**
	 * Given relevant configuration strings, attempts to create a device capable of communicating with the device inside
	 * the house.
	 * \param[in] info JSON string representing the device configuration.
	 * \param[in] house_json JSON string representing the house configuration.
	 * \param[in] frame TimeFrame for the Device to use for time stamping
	 * \param[out] Instance of Device representing the requested device, or null if the inputs are invalid.
	 */
	public static Device CreateDevice(FullID id, string house_json, string device_json, TimeFrame frame)
	{
		var inp = new HouseInput(house_json, device_json);
		var outp = new HouseOutput(house_json, device_json);
		var device = Interfaces.DeserializeDevice(device_json, inp, outp, frame);
		if(device != null)
		{
			device.ID = id;
		}
		return device;
	}
}

}
