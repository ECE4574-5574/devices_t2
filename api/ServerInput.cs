/**
 * Input for reading device state from the Server API
 */
using System;

namespace api
{
public class ServerInput : IDeviceInput
{
	public ServerInput(Connection Uri) : base(Uri, Null) 
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
