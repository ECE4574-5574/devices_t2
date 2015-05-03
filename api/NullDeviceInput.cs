using System;
using System.Reflection;

namespace api
{
public class NullDeviceInput : IDeviceInput
{
	public NullDeviceInput()
	{
	}

	public bool read(Device dev)
	{
		dev.LastUpdate = dev.Frame.now();
		return true;
	}
}
}

