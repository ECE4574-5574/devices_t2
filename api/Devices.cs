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
using System.Collections.Generic;

namespace api
{
    /**
     * Represents a device which can be enabled or disabled.
     */
    interface IEnableable
    {
        bool Enabled
        {
            get;
            set;
        }
    };

    /**
     * Interface defining an object which can accept some number of discrete states
     */
    interface IDiscreteSetting
    {
        Int64 State
        {
            get;
            set;
        }

        Int64 MinState();
        Int64 MaxState();
    };

    /**
     * Interface defining how to set a set point for the behavior of a given Device
     */
    interface ISetPointable
    {
        Int64 SetPoint
        {
            get;
            set;
        }
    }
    /**
     * Base class representing the common parameters for any given device. All Devices inherit from this
     */
    public abstract class Device
    {
		public UInt64 HouseID
		{
			get;
			set;
		}
		public UInt64 RoomID
		{
			get;
			set;
		}

		public UInt64 DeviceID
		{
			get;
			set;
		}
		public string Name
		{
			get;
			set;
		}
		public string Type
		{
			get;
			set;
		}
    }

	/**
	 * Class representing a garage door in the house.
	 */
    public class GarageDoor : Device, IEnableable
    {
        public bool Enabled
        {
            get;
            set;
        }
    }

	/**
	 * A ceiling fan, which
	 */
    public class CeilingFan : Device, IEnableable, IDiscreteSetting
    {
        // Attributes
        public bool Enabled
        {
            get;
            set;
        }
        public Int64 State
        {
            get;
            set;
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

    public class Refrigerator : Device, ISetPointable
    {
        public Int64 SetPoint
        {
            get;
            set;
        }
    }

    public class AlarmSystem : Device, IEnableable
    {
        public bool Enabled
        {
            get;
            set;
        }
    }

    public class Light : Device, IEnableable
    {
        public bool Enabled
        {
            get;
            set;
        }
    }

    public class MotionSensor : Device, IEnableable
    {
        public bool Enabled
        {
            get;
            set;
        }
    }
    public class Thermostat : Device, ISetPointable
    {
        public Int64 SetPoint
        {
            get;
            set;
        }
        public Int64 CurrentTemp
        {
            get
            {
                return _temp;
            }
            set
            {
            } //Cannot set temperature externally
        }

        protected Int64 _temp;
    }
}
