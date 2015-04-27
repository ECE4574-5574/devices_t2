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
			if(_in.read(this))
			{
				_update_ok = true;
				return _id;
			}
			else
			{
				_update_ok = false;
				return _id;
			}
		}
		set
		{
			_id = value;
			_out.write(this);
		}
	}

	public DateTime LastUpdate
	{
		get
		{
			if(_in.read(this))
			{
				_update_ok = true;
				return _last_time;
			}
			else
			{
				_update_ok = false;
				return System.DateTime.MinValue;
			}
					
		}
		set
		{
			_last_time = value;
			_out.write(this);
		}
	}

	/**
	 * User friendly name for this device.
	 */
	public string Name
	{
		get
		{
			if(_in.read(this))
			{
				_update_ok = true;
				return _name;
			}
			else
			{
				_update_ok = false;
				return _name;
			}
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
		set;
	}

	[JsonIgnore]
	public TimeFrame Frame
	{
		get
		{
			if(_in.read(this))
			{
				_update_ok = true;
				return _frame;
			}
			else
			{
				_update_ok = false;
				return _frame;
			}
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
		//TODO: This function should grab the latest state of the device
		//using the IDeviceInput, without worrying about any particular parameter
		if(_in.read(this))
		{
			_update_ok = true;
			return _update_ok;
		}
		else
		{
			_update_ok = false;
			return _update_ok;
		}
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
