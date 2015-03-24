/*
STUB implementation of the device list in C Sharp
Contributors: Pedro Sorto, Steven Cho, Dong Nan, Aakruthi Gopisetty, Kara Dodenhoff and Danny Mota
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

        static void Main(string[] args)
        {
            // testing garage door object to get ID, Device Name, Room ID, and Status
            GarageDoor boo = new GarageDoor();

            boo.addDevice(2, "Garage1", 4, false);
            Console.WriteLine(boo.getid());
            Console.WriteLine(boo.getDeviceName());
            Console.WriteLine(boo.getRoomID());
            Console.WriteLine(boo.getStatus());

            sprinkler boo2 = new sprinkler();

            boo2.addDevice(4, "Sprinkler1", 1, false);
            Console.WriteLine(boo2.getid());
            Console.WriteLine(boo2.getDeviceName());
            Console.WriteLine(boo2.getRoomID());
            Console.WriteLine(boo2.getStatus());
            boo2.turnOn();
            Console.WriteLine(boo2.getStatus());

        }
    }
    abstract class device
    {
        protected int id;
        protected string deviceName;
        protected int roomID;
        protected bool status;
        public device()
        {
            id = 0;
            deviceName = "New Device";
            roomID = 0;
            status = false;
        }
        public virtual void addDevice(int id, string devName, int rid, bool status) { ;}
        public virtual int getid() { return id; }
        public virtual string getDeviceName() { return deviceName; }

        public virtual int getRoomID() { return roomID;}

        public virtual bool getStatus() {return status;}
        public virtual void setStatus(bool state) {;}
    }
    class GarageDoor : device
    {
        // Attributes
        public override void addDevice(int id2, string devName, int rid, bool state)
        {
            //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
        public override int getid() {return id;}
        public override string getDeviceName() {return deviceName;}
        public override int getRoomID() { return roomID; }
       
        public override bool getStatus(){return status;}  // closed = false, open = true 
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
    class CelingFan : device
    {
        // Attributes
       
        public int speed;
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

        public override bool getStatus() { return status; }  // off = false, on = true 


        public override void setStatus(bool state)
        {
            status = state;
        }
        public int getSpeed()
        {
            return speed; // speed from [,]
        }
        public void setSpeed(int spd)
        {
            speed = spd;
        }
        public void turnOn()
        {
            // turn on the fan
        }

        public void turnOff()
        {
            // turn off the fan
        }

    } /* end class CelingFan */

    class Refrigerator : device
    {
        // Attributes
        public int temp; // temperature
        public int filter_status;
        public bool icemaker_status;
        public override void addDevice(int id2, string devName, int rid, bool state)
        {
            //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }
        public int getTemp() // return temperature
        {
            return temp;
        }
        public void setTemp(int t)  // set temperature
        {
            temp = t;
        }
        public void turnOn()
        {
            // turn on device
        }
        public void turnOff()
        {
            // turn off device
        }
        
    } /* end class Refrigerator */
    class sprinkler : device
    {
        public bool sprinkler_status;

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
        public void turnOn()
        {
            status = true;
        }

        public void turnOff()
        {
            status = false;
        }

    } /* end class Sprinkler */

    class alarmSystem : device
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

    class lights : device
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
        public int brightness;
        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}

        public int getBrightness()
        {
            return brightness;
        }
        public void setBrightness(int blevel)
        {
            brightness = blevel;
        }

        public void turnOn()
        {
            status = true;
        }


        public void turnOff()
        {
            status = false;
        }
    } /* end class Lights */

    class motionSensor : device
    {
        public override void addDevice(int id2, string devName, int rid, bool state)
        {
            //base.addDevice(id, devName, rid, status);
            id = id2;
            deviceName = devName;
            roomID = rid;
            status = state;
        }

        public bool motion;
        public override int getid() { return id; }
        public override string getDeviceName() { return deviceName; }
        public override int getRoomID() { return roomID; }

        public override bool getStatus() { return status; }  // closed = false, open = true 
        public override void setStatus(bool state) { ;}

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

    class microwave : device
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


        public microwave() { }

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

    class HVAC :device
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

        public int getTemperature()
        {
            return tempCurrent;
        }

        public int setTemperature()
        {
            tempCurrent = tempDesired;
            return tempCurrent;
        }

        public int getHumidity()
        {
            return humidityCurrent;
        }

        public int setHumidity()
        {
            humidityCurrent = humidityDesired;
            return humidityCurrent;
        }

        public bool getRunningStatus()
        {
            return isRunning;
        }
    } /* end class HVAC */

    class doorLocks : device
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
        //public void doorlocks() { }

        public bool getLockedStatus()
        {
            return isLocked;
        }

        public void toggleDoorLocks()
        {
            if (isLocked == true)
                isLocked = false;
            else
                isLocked = true;
        }
    } /* end class doorLocks */

    class windows : device
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
        public windows() { }

        public void Open()
        {
            open = true;
        }

        public void Close()
        {
            open = false;
        }

        public bool isOpen()
        {
            if (open == true)
                return true;
            else
                return false;
        }


        public void setLockable(bool lockable)
        {
            if (lockable == true)
                lockable = false;
            else
                lockable = true;
        }

        public bool isLockable()
        {
            return lockable;
        }

        public double GetWidth()
        {
            return width;
        }

        public double GetHeight()
        {
            return height;
        }

        public void SetHeight(double height)
        {
            this.height = height;
        }

        public void SetWidth(double width)
        {
            this.width = width;
        }
    } /* end class Windows */
}

