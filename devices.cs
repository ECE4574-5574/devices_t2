abstract public class device
{
    public int deviceType;
    public int deviceID;

	public device()
	{} 
   
   abstract public void getType()
    {
		return deviceType;
    }

    abstract public void getID()
    {
		return deviceID;
    }
	

	abstract public class sprinkler
	{

	public bool sprinkler_status;

	public sprinkler()
	{
	//initialization 
	}

	abstract public void turnOn
	{
		sprinkler_status = true;
	}
	
	abstract public void turnOff
	{
		sprinkler_status = false;
	}
	
	} /* end class Sprinkler */

	abstract public class alarmSystem
	{

	public bool alarm_system_status;
	public bool arm_status;

	public alarmSystem()
	{}

	abstract public void turnOn()
	{
	Console.WriteLine("Alarm is on.");
	alarm_system_status = true; 
	}
	abstract public void turnOff()
	{
	Console.WriteLine("Alarm is off.");
	alarm_system_status = false; 
	}

	abstract public bool getAlarmStatus()
	{
	return alarm_system_status;
	}

	abstract public void armAlarm()
	{
	Console.WriteLine("Alarm is armed.");
	arm_status = true; 
	}
	abstract public void disarmAlarm()
	{
	Console.WriteLine("Alarm is disarmed.");
	arm_status = false;
	}

	abstract public bool getArmStatus()
	{
	return arm_status;
	}
	} /* end class Alarm System */

	abstract public class Lights
	{
	public bool status;
	public int brightness_status;

	public Lights()
	{
	}

	abstract public int getBrightness()
	{
	return brightness_status;
	}

	abstract public void setBrightness()
	{
	}

	abstract public void turnOn()
	{
	status = true;
	}


	abstract public void turnOff()
	{
	status = false;
	}

	abstract public bool getStatus()
	{
	return status;
	}
	} /* end class Lights */

	abstract public class motionSensor
	{
	public bool status;
	public bool motion;

	public motionSensor()
	{}

	abstract public int getMotion()
	{
	return motion;
	}

	abstract public void turnOnSensor()
	{
	status = true;
	}

	abstract public void turnOffSensor()
	{
	status = false;
	}

	} /* end class motion sensor */


} /* end class Device */
