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
		}

		public FullID ID
		{
			get
			{
				if (_in.read(this))
					return _id;
				else
					return null;
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
				if (_in.read(this))
					return _last_time;
				else
					return System.DateTime.MinValue;
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
				if (_in.read(this))
					return _name;
				else
					return null;
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
				if (_in.read(this))
					return _frame;
				else
					return null;
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
			return _in.read(this);
		}

		protected IDeviceInput _in;
		protected IDeviceOutput _out;
		protected TimeFrame _frame;
		protected DateTime _last_time;
		protected string _name;
		protected FullID _id;
	}

}
