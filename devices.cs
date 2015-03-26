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
    class DeviceAPI
    {
        
    }
    abstract class device
    {
        protected int id {get; set;}
        protected string deviceType { get; set; }
        protected bool status { get; set; }
        protected string stringname;
        protected string stringname2; //concatenated string
        protected int index;
        public device()
        {
            id = 0; 
            deviceType = "New Device";
            status = false;  // open/close, on/off
        }
        public virtual void addDevice(int id, string devType, bool status) { ;}
        public virtual int getid() { return id; }
        public virtual string getDeviceType() { return deviceType; }
        public virtual bool getStatus() {return status;}
        public virtual void setStatus(bool state) {;}
        public void add(int id, string devType, bool status)
        {/*
            stringname = "boo";
            stringname2 = String.Concat("boo" , index);
            index++;
            switch(devType){
                case "garage": GarageDoor boo = new GarageDoor();
                break;

                case "lights": lights boo2= new lights();
                break;   
        }
                    */
        }
    }
    class GarageDoor : device
    {
        // Attributes
        public override void addDevice(int id2, string devType, bool state)
        {
           //base.addDevice(id, devType, rid, status);
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() {return id;}
        public override string getDeviceType() {return deviceType;}
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
        public override void addDevice(int id2, string devType, bool state)
        {
            //base.addDevice(id, devType, rid, status);
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
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
            status = true;
        }

        public void turnOff()
        {
            // turn off the fan
            status = false;
        }

    } /* end class CelingFan */

    class Refrigerator : device
    {
        // Attributes
        private int temp; // temperature
        private int filter_status;
        private bool icemaker_status;
        public override void addDevice(int id2, string devType, bool state)
        {
            //base.addDevice(id, devType, rid, status);
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // off = false, on = true 
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
            status = true;
        }
        public void turnOff()
        {
            // turn off device
            status = false;
        }
        
    } /* end class Refrigerator */

    class alarmSystem : device
    {
        //public bool alarm_system_status;
        //public bool arm_status;
        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 
        public void turnOn()
        {
            Console.WriteLine("Alarm is on.");
            status = true;
        }

        public void turnOff()
        {
            Console.WriteLine("Alarm is off.");
            status = false;
        }
    } /* end class Alarm System */

    class lights : device
    {
        private int brightness;

        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

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
        private bool motion;
        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

        public bool getMotion()
        {
            return motion;
        }

        public void turnOn()
        {
            status = true;
        }

        public void turnOff()
        {
            status = false;
        }

    } /* end class motion sensor */

    class microwave : device
    {
        
        private int wattage;
        private double time;
        private string[] command;
        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

        public microwave() {wattage = 0; time = 0; command = null; }

        public string[] getCommands()
        {
            return command;
        }

        public void SendCommand(string command)
        {
            // this.command.Insert(this.command.Count, command);
        }

        public void TurnOn()
        {
            status = true;
        }

        public void TurnOff()
        {
            status = false;
        }

        public void SetPower(int power)
        {
            wattage = power;
        }

        public void setTimer(int minutes, int seconds)
        {
            time = minutes + seconds / 60;
        }
    } /* end class microwave*/

    class HVAC :device
    {


        private int tempCurrent;
        private int tempDesired;
        private int humidityCurrent;
        private int humidityDesired;

        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

        public int getTemp()
        {
            return tempCurrent;
        }

        public int setTemp()
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

    } /* end class HVAC */

    class doorLocks : device  // NOT IN USE
    {
        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

        private bool isLocked;
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

    class windows : device  // NOT IN USE
    {
        private bool lockable;
        private double height;
        private double width;
        public override void addDevice(int id2, string devType, bool state)
        {
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

        //public ArrayList  myDoor/Window;

        //public ArrayList  my;
        public windows() { }

        public void Open()
        {
            status = true;
        }

        public void Close()
        {
            status = false;
        }

        public bool isOpen()
        {
            if (status == true)
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
    class sprinkler : device  // NOT IN USE
    {
        //public bool sprinkler_status;

        public override void addDevice(int id2, string devType, bool state)
        {
            //base.addDevice(id, devType, rid, status);
            id = id2;
            deviceType = devType;
            status = state;
        }
        public override int getid() { return id; }
        public override string getDeviceType() { return deviceType; }

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
}


