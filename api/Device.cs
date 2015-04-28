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
		//Force sane defaults, so we can assume these are always valid
		if(inp == null)
		{
			inp = new NullDeviceInput();
		}
		if(outp == null)
		{
			outp = new NullDeviceOutput();
		}
		if(frame == null)
		{
			frame = new TimeFrame();
		}
		_in = inp;
		_out = outp;
		_frame = frame;
		_last_time = DateTime.MinValue; //Set to minimum possible time
		_id = new FullID();
		_name = "";
		_update_ok = false;
	}

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

	public string Class
	{
		get;
		protected set;
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

	protected IDeviceInput _in;
	protected IDeviceOutput _out;
	protected TimeFrame _frame;
	protected DateTime _last_time;
	protected string _name;
	protected FullID _id;
	private bool _update_ok;
}

}
