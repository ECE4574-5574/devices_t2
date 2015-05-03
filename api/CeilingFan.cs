/*
 * Declaration of all devices and the unique classes that inherit from device class that hold parameters and 
 * characteristics of each device.
 * Contributors:
 *   Pedro Sorto
 *   Steven Cho
 *   Dong Nan
 *   Aakruthi Gopisetty
 *   Kara Dodenhoff
 *   Danny Mota
 *   Jason Ziglar <jpz@vt.edu>
*/
using System;
using Hats.Time;

namespace api
{

/**
 * A ceiling fan, which can be turned on and off, and also have a speed setting
 */
public class CeilingFan : Device, IEnableable, IDiscreteSetting
{
	protected bool _enabled;
	protected Int64 _state;
	public CeilingFan(IDeviceInput inp, IDeviceOutput outp, TimeFrame frame) :
	base(inp, outp, frame)
	{
		Enabled = false;
		State = 0;
		Class = "CeilingFan";
	}
	public bool Enabled
	{
		get
		{
			UpdateOk = _in.read(this);
			return _enabled;
		}
		set
		{
			_enabled = value;
			_out.write(this);
		}
	}

	public Int64 State
	{
		get
		{
			UpdateOk = _in.read(this);
			return _state;
		}
		set
		{
			_state = value;
			_out.write(this);
		}
	}

	public Int64 MinState()
	{
		return 0;
	}

	public Int64 MaxState()
	{
		return 100;
	}
}
}

