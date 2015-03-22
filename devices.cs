/*
STUB implementation of the device list in C Sharp
Contributors: Pedro Sorto, Steven Cho, Dong Nan.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASSIGN5TEAM2
{
    class Program
    {
        class Program
    {
        static void Main(string[] args)
        {
			// testing to create new device, and add/set device then prints ID, DeviceName, Room ID, Status.
            GarageDoor boo = new GarageDoor();
            boo.addDevice(2, "Garage1", 4, false);
            Console.WriteLine(boo.getid());
            Console.WriteLine(boo.getDeviceName());
            Console.WriteLine(boo.getRoomID());
            Console.WriteLine(boo.getStatus());
        }
    }
	
    }
    abstract class device
    {
        protected int id;
        protected string deviceName;
        protected int roomID;
        protected bool status;

        public virtual int getid() { return id; }
        public virtual string getDeviceName() { return deviceName; }
        public virtual int getRoomID() { return roomID; }
        public virtual bool getStatus() { return status; }
        public virtual void setStatus(bool state) {;}
    }

    abstract class sprinkler : device
	{
	
	public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }

	public bool sprinkler_status;

	public sprinkler()
	{
	//initialization 
	}

	public void turnOn()
	{
		sprinkler_status = true;
	}
	
	public void turnOff()
	{
		sprinkler_status = false;
	}
	
	} /* end class Sprinkler */

	abstract class alarmSystem : device
	{
	
		public override void addDevice(int id2, string devName, int rid, bool state)
			{
			   //base.addDevice(id, devName, rid, status);
				id = id2;
				deviceName = devName;
				roomID = rid;
				status = state;
			}

	    public bool alarm_system_status;
	    public bool arm_status;

        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}

	    public alarmSystem()
	    {}

	    public void turnOn()
	    {
	    Console.WriteLine("Alarm is on.");
	    alarm_system_status = true; 
	    }
	
        public void turnOff()
	    {
	    Console.WriteLine("Alarm is off.");
	    alarm_system_status = false; 
	    }

	    public bool getAlarmStatus()
	    {
	    return alarm_system_status;
	    }

	    public void armAlarm()
	    {
	    Console.WriteLine("Alarm is armed.");
	    arm_status = true; 
	    }
	
        public void disarmAlarm()
	    {
	    Console.WriteLine("Alarm is disarmed.");
	    arm_status = false;
	    }

	    public bool getArmStatus()
	    {
	    return arm_status;
	    }
	} /* end class Alarm System */

	abstract class Lights : device
	{
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
			
	    public bool status;
	    public int brightness_status;
        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}


	    public Lights()
	    {
	    }

	    public int getBrightness()
	    {
	    return brightness_status;
	    }

	    public void setBrightness()
	    {
	    }

	    public void turnOn()
	    {
	    status = true;
	    }


	    public void turnOff()
	    {
	    status = false;
	    }

	    public bool getStatus()
	    {
	    return status;
	    }
	} /* end class Lights */

	abstract class motionSensor : device
	{
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
	
	    public bool status;
	    public bool motion;
        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}


	    public motionSensor()
	    {}

	    public bool getMotion()
	    {
	    return motion;
	    }

	    public void turnOnSensor()
	    {
	    status = true;
	    }

	    public void turnOffSensor()
	    {
	    status = false;
	    }

	} /* end class motion sensor */

    abstract class microwave : device
	{
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
	
        public int wattage;
		public double time;
		public bool on;
		public string[] command;
        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}


        public microwave() {}

        public string[] GetCommands()
        {
            return command;
        }

        public void SendCommand(string command)
        {
           // this.command.Insert(this.command.Count, command);
        }

        public bool TurnOn()
        {
            on = true;
            return on;
        }

        public bool TurnOff()
        {
            on = false;
            return on;
        }

        public void SetPower(int power)
        {
            wattage = power;
        }

        public bool SetTimer(int minutes, int seconds)
        {   
            time = minutes + seconds / 60;
            return true;
        }
    } /* end class microwave*/


    class GarageDoor : device
    {
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
	
        // Attributes
        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public void open()
        {
            // implement opens until max height
            status = true;
        }

        public void close()
        {
            // implement closes until height is 0
            status = false;
        }/* end class GarageDoor */

    }

    
    abstract class HVAC
    {
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
	
        public int tempCurrent;

        public int tempDesired;

        public int humidityCurrent;

        public int humidityDesired;

        public bool isRunning;

        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}

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
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }

        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}

        public bool isLocked;

        public void doorlocks() {}

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
	
		public override void addDevice(int id2, string devName, int rid, bool state)
        {
           //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
		
        public bool lockable;

        public double height;

        public double width;

        public bool open;

        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}

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
            this.width = width;
        }
    } /* end class Windows */

}
