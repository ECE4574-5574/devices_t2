/*
STUB implementation of the device list in C Sharp
Contributors: Pedro Sorto, Steven Cho, Dong Nan.
*/

abstract class device
{
    public int deviceName;
    public int deviceID;
	public int roomID;
	
	public device()
	{} 
   
   abstract public void getName()
    {
		return deviceName;
    }

    abstract public void getID()
    {
		return deviceID;
    }
	
	abstract public void getRoomID()
	{
		return roomID;
	}

	abstract class sprinkler
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

	abstract class alarmSystem
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

	abstract class Lights
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

	abstract class motionSensor
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

    abstract class microwave {
        public int wattage;

        public double time;

        public bool on;

        public string[] command;

        public microwave() {}

        abstract public string[] GetCommands()
        {
            return command;
        }

        abstract public  bool SendCommand(string command)
        {
            this.command.Insert(this.command.Count, command);
        }

        abstract public  bool TurnOn()
        {
            on = true;
            return on;
        }

        abstract public  bool TurnOff()
        {
            on = false;
            return on;
        }

        abstract public  bool SetPower(int power)
        {
            wattage = power;
        }

        abstract public  bool SetTimer(int minutes, int seconds)
        {   
            time = minutes + seconds / 60;
            return true;
        }
    } /* end class microwave*/

    abstract class HVAC
    {
        public int tempCurrent;

        public int tempDesired;

        public int humidityCurrent;

        public int humidityDesired;

        public bool isRunning;

        public HVAC() {}

        abstract public int getTemperature()
        {
            return tempCurrent;
        }

        abstract public int setTemperature()
        {
            tempCurrent = tempDesired;
            return tempCurrent;
        }

        abstract public int getHumidity()
        {
            return humidityCurrent;
        }

        abstract public int setHumidity()
        {
            humidityCurrent = humidityDesired;
            return humidityCurrent;
        }

        abstract public bool getRunningStatus()
        {
            return isRunning;
        }
    } /* end class HVAC */

    abstract class doorLocks
    {
        public bool isLocked;

        public doorlocks() {}

        abstract public bool getLockedStatus()
        {
            return isLocked;
        }

        abstract public  void toggleDoorLocks()
        {
            if (isLocked == true)
                isLocked = false;
            else
                isLocked = true;
        }
    } /* end class doorLocks */

    abstract class windows
    {
        public bool lockable;

        public double height;

        public double width;

        public bool open;

        //public ArrayList  myDoor/Window;

        //public ArrayList  my;
        public windows() {}

        abstract public  void Open()
        {
            open = true;
        }

        abstract public  void Close()
        {
            open = false;
        }

        abstract public bool isOpen()
        {
            if(open == true)
                return true;
            else
                return false;
        }

        abstract public  void SetLockable(bool lockable)
        {
            if(lockable == true)
                lockable = false;
            else
                lockable = true;
        }

        abstract public bool isLockable()
        {
            return lockable;
        }

        abstract public double GetWidth()
        {
            return width;
        }

        abstract public double GetHeight()
        {
            return height;
        }

        abstract public void SetHeight(double height)
        {
            this.height = height;
        }

        abstract public void SetWidth(double width)
        {
            this.height = height;
        }
    } /* end class Windows */

}
 /* end class Device */
