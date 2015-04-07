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
	/**
	 * Discrete State of this device.
	 */
	Int64 State
	{
		get;
		set;
	}

	//! Minimum value this particular device will accept
	Int64 MinState();
	//! Maximum value this device will accept
	Int64 MaxState();
};

/**
 * Interface defining how to set a set point for the behavior of a given Device
 */
interface ISetPointable
{
	/**
	 * Target set point of this device. For example, the set point of a thermostat
	 */
	Int64 SetPoint
	{
		get;
		set;
	}
}


public class DeviceID
{
	/**
	 * Identifier for the house this device is contained within
	 */
	public UInt64 House
	{
		get;
		set;
	}
	/**
	 * Identifier for the room in which a device is contained.
	 * The value 0 represents a device which isn't assigned to a specific room.
	 */
	public UInt64 Room
	{
		get;
		set;
	}
	/**
	 * House specific identifier for this device. This requires the previous two
	 * IDs in order to uniquely identify this particular device.
	 */
	public UInt64 Device
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
	public DeviceID ID
	{
		get;
		set;
	}

	/**
	 * User friendly name for this device.
	 */
	public string Name
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
 * A ceiling fan, which can be turned on and off, and also have a speed setting
 */
public class CeilingFan : Device, IEnableable, IDiscreteSetting
{
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

/**
 * A smart refrigerator which lets you get/set the temperature setpoint
 */
public class Refrigerator : Device, ISetPointable, IDiscreteSetting
{
	public Int64 SetPoint
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
		return -100;
	}
	public Int64 MaxState()
	{
		return 200;
	}
}

/**
 * Alarm which can be enabled/disabled
 */
public class AlarmSystem : Device, IEnableable
{
	public bool Enabled
	{
		get;
		set;
	}
}

/**
 * Binary light switch.
 */
public class LightSwitch : Device, IEnableable
{
	public bool Enabled
	{
		get;
		set;
	}
}

/**
 * Thermostat for a house, which can have a setpoint and a measured value
 */
public class Thermostat : Device, IEnableable, ISetPointable, IDiscreteSetting
{
	public bool Enabled
	{
		get;
		set;
	}

	public Int64 SetPoint
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
		return -100;
	}
	public Int64 MaxState()
	{
		return 200;
	}
}
}
