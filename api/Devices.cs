/*
Declaration of all devices and the unique classes that inherit from device class that hold parameters and 
characteristics of each device.
STUB implementation of the device list in C Sharp
Contributors: Pedro Sorto, Steven Cho, Dong Nan, Aakruthi Gopisetty, Kara Dodenhoff and Danny Mota
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Tuple<UInt64, UInt64> ID
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

    public class GarageDoor : Device, IEnableable
    {
        public bool Enabled
        {
            get;
            set;
        }
    }
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
