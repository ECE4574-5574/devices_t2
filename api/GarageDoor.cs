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
 * Class representing a garage door in the house.
 */
public class GarageDoor : Device, IEnableable
{
	protected bool _enabled;
	public GarageDoor(IDeviceInput inp, IDeviceOutput outp, TimeFrame frame) :
	base(inp, outp, frame)
	{
		Enabled = true;
		Class = "GarageDoor";
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
}

}
