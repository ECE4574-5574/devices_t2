/**
 * Input for reading device state from the Server API
 */
using System;

namespace api
{
public class ServerInput : IDeviceInput
{
	public ServerInput(string server_url) 
	{
	}

	/**
	 * Updates device from the server
	 */
	public bool read(Device dev)
	{
		//TODO: Implement this function
		return true;
	}
}

}
