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
 * Thermostat for a house, which can have a setpoint and a measured value
 */
public class Thermostat : Device, IEnableable, ISetPointable<Temperature>, IReadable<Temperature>
{
	protected bool _enabled;
	protected Temperature _setpoint;
	protected Temperature _temp;

	public Thermostat(IDeviceInput inp, IDeviceOutput outp, TimeFrame frame) :
	base(inp, outp, frame)
	{
		Enabled = false;
		SetPoint = new Temperature()
		{
			Temp = 0
		};
		Value = new Temperature()
		{
			Temp = 0
		};
		Class = "Thermostat";
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

	public Temperature SetPoint
	{
		get
		{
			UpdateOk = _in.read(this);
			return _setpoint;
		}
		set
		{
			_setpoint = value;
			_out.write(this);
		}
	}

	public Temperature Value
	{
		get
		{
			UpdateOk = _in.read(this);
			return _temp;
		}
		protected set
		{
			_temp = value;
			_out.write(this);
		}
	}

	public Int64 MinState()
	{
		return -100;
	}
	public Int64 MaxState()
	{
		return 200;
	}
}

}
