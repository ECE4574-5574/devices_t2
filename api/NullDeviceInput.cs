using System;

namespace api
{
public class NullDeviceInput : IDeviceInput
{
	public NullDeviceInput()
	{
	}

	public bool read(Device dev)
	{
		return true;
	}
}
}

