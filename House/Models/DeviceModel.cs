using System;
using System.Collections.Generic;
using api;

namespace House
{

public sealed class DeviceModel
{
	private static readonly Lazy<DeviceModel> _model = new Lazy<DeviceModel>(() => new DeviceModel());

	public static DeviceModel Instance
	{
		get
		{
			return _model.Value;
		}
	}

	public DeviceModel()
	{
		Devices = new List<Device>();
		_responding = false;
	}

	public List<Device> Devices
	{
		get;
		set;
	}

	public bool Responding
	{
		get
		{
			return _responding;
		}
		set
		{
			_responding = value;
		}
	}

	private volatile bool _responding;
}

}
