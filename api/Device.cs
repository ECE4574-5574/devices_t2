/**
 * Base class for all devices in the HATS system.
 * \author Jason Ziglar <jpz@vt.edu>
 */
using System;
using Hats.Time;
using Newtonsoft.Json;

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
        _last_time = DateTime.MinValue; //Set to minimum possible time
		ID = new FullID();
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

	public string Class
	{
		get;
		set;
	}

	[JsonIgnore]
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

	/**
	 * Grabs the latest available information about this device, and updates
	 * internal state to match it. This should also update LastUpdated as a post condition.
	 * \param[out] Flag indicating success
	 */
	public bool update()
	{
		//TODO: This function should grab the latest state of the device
		//using the IDeviceInput, without worrying about any particular parameter
		return true;
	}

	protected IDeviceInput _in;
	protected IDeviceOutput _out;
	protected TimeFrame _frame;
	protected DateTime _last_time;
}

}
