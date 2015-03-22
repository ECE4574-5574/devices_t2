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

	public int sprinkler_model;

	public sprinkler()
	{
	//initialization 
	}

	abstract public int getSprinklerModel()
	{
	return sprinkler_model;
	}

	abstract public class Client_Sprinkler : Sprinkler
	{

	public Boolean water_status;
	public Boolean sprinkler_state;

	public  Boolean sendWaterStatus()
	{

	}

	public  Boolean sendSprinklerState()
	{

	}

	public  void setSprinklerState()
	{

	}
	} /* end class Client_Sprinkler */

	abstract public class server_sprinkler : Sprinkler
	{
	public server_sprinkler()
	{}

	abstract public void requestWaterStatus()
	{

	}

	abstract public void requestSprinklerState()
	{

	}

	abstract public void sendSprinklerState()
	{

	}
	} /* end class Server_Sprinkler */


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

	abstract public class lights
	{
	public bool status;
	public int brightness_status;

	public lights()
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
