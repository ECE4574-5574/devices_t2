/**
 * Base class for all devices in the HATS system.
 * \author Jason Ziglar <jpz@vt.edu>
 */
using System;
using Hats.Time;

namespace api
{
/**
 * Base class representing the common parameters for any given device. All Devices inherit from this
 */
public abstract class Device
{
	public Device(IDeviceInput inp, IDeviceOutput outp, TimeFrame frame)
	{
		_in = inp;
		_out = outp;
		_frame = frame;
        _last_time = new DateTime(DateTime.MinValue); //Set to minimum possible time
	}

	public FullID ID
	{
		get;
		set;
	}

    public DateTime LastUpdate
    {
        get
		{
			return _last_time;
		}
        set
		{
			_last_time = value;
		}
    }

	/**
	 * User friendly name for this device.
	 */
	public string Name
	{
		get;
		set;
	}

	public TimeFrame Frame
	{
		get
		{
			return _frame;
		}
		set
		{
			_frame = value;
		}
	}

	protected IDeviceInput _in;
	protected IDeviceOutput _out;
	protected TimeFrame _frame;
	protected DateTime _last_time;
}
}

