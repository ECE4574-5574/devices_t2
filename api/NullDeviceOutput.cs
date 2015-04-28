using System;

namespace api
{
public class NullDeviceOutput : IDeviceOutput
{
	public NullDeviceOutput()
	{
	}

	public bool write(Device dev)
	{
		return true;
	}
}
}

