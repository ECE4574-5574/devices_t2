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
 * Binary light switch.
 */
public class LightSwitch : Device, IEnableable, IReadable<Light>
{
	public LightSwitch(IDeviceInput inp, IDeviceOutput outp, TimeFrame frame) :
	base(inp, outp, frame)
	{
		_enabled = false;
		_light = new Light();
		Class = "LightSwitch";
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
			if(value)
			{
				_light.Brightness = 1.0;
			}
			else
			{
				_light.Brightness = 0.0;
			}
			_enabled = value;
			_out.write(this);
		}
	}

	public Light Value
	{
		get
		{
			UpdateOk = _in.read(this);
			return _light;
		}
		protected set
		{
			_light = value;
			_out.write(this);
		}
	}
	protected Light _light;
	protected bool _enabled;
}

}
