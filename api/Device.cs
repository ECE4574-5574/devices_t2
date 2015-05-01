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
		resetIO(inp, outp);
		if(frame == null)
		{
			frame = new TimeFrame();
		}
		_frame = frame;
		_last_time = DateTime.MinValue; //Set to minimum possible time
		_id = new FullID();
		_name = "";
		_update_ok = false;
	}

	/**
	 * Flag indicating if the previous attempt to communicate with a remote
	 * authority succeeded.
	 */
	public bool UpdateOk
	{
		get
		{
			return _update_ok;
		}
		private set
		{
			_update_ok = value;
		}
	}

	/**
	 * Unique identifier for this device in the HATS system.
	 */
	public FullID ID
	{
		get
		{
			_update_ok = _in.read(this);
			return _id;
		}
		set
		{
			_id = value;
			_update_ok = _out.write(this);
		}
	}

	/**
	 * The last time this Device was updated from a remote read.
	 */
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
		get
		{
			_update_ok = _in.read(this);
			return _name;
		}
		set
		{
			_name = value;
			_out.write(this);
		}

	}

	/**
	 * Name of this class for reconstructions.
	 */
	public string Class
	{
		get;
		protected set;
	}

	/**
	 * TimeFrame for computing "now".
	 */
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
			_out.write(this);
		}
	}

	/**
	 * Grabs the latest available information about this device, and updates
	 * internal state to match it. This should also update LastUpdated as a post condition.
	 * \param[out] Flag indicating success
	 */
	public bool update()
	{
		_update_ok = _in.read(this);
		return _update_ok;
	}

	/**
	 * Rests internal IO objects for this Device. With no arguments, the
	 * device will no longer communicate with any remote authority, making this object
	 * useful for storing old results/dumb usage.
	 * \param[in] inp IDeviceInput instance, defaults to a NullDeviceInput.
	 * \param[out] outp IDeviceOutput instance, defaults to a NullDeviceOutput.
	 */
	public void resetIO(IDeviceInput inp = default(IDeviceInput), IDeviceOutput outp = default(IDeviceOutput))
	{
		//Force sane defaults, so we can assume these are always valid
		if(inp == null)
		{
			inp = new NullDeviceInput();
		}
		if(outp == null)
		{
			outp = new NullDeviceOutput();
		}
		_in = inp;
		_out = outp;
	}

	protected IDeviceInput _in;
	protected IDeviceOutput _out;
	protected TimeFrame _frame;
	protected DateTime _last_time;
	protected string _name;
	protected FullID _id;
	private bool _update_ok;
}

}
