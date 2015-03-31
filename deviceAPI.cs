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
//using MySql.Data.MySqlClient;
namespace ASSIGN5TEAM2
{
    class DeviceAPI
    {

    }
    public abstract class device
    {
        protected int id;
        protected string deviceName;
        protected string deviceType;
        protected int roomid;
        protected bool status;

        protected string [] parsed;
        protected device(int id2 = 0, string devName = "Nameless", string devType = "Type" , int rid = 0, bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
        }
        
        public virtual void addDevice(int id, string devType, bool status) { ;}
        public virtual int getid() { return id; }
        public virtual void setid(int id2) { id = id2; }
        public virtual int getroomid() { return id; }
        public virtual void setroomid(int rid2) { roomid = rid2; }
        public virtual string getDeviceName() { return deviceType; }
        public virtual void setDeviceName(string devName) { deviceName = devName; }
        public virtual string getDeviceType() { return deviceType; }
        public virtual void setDeviceType(string devType) { deviceType = devType; }
        public virtual bool getStatus() { return status; }
        public virtual void setStatus(bool state) { status = state; }
        /*
        public void parser(string jsoncmd)
        {
            return 
        }*/
        //public void accessDevAPI(string query, MySqlConnection connection) { ;}
    }
    class Garagedoor : device
    {
        // Attributes
      
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type" , int rid = 0, bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // true/false, open/close, on/off
        }

        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
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
    class Ceilingfan : device
    {
        // Attributes

        public int speed;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type" , int rid = 0, bool state = false, int spd = 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
            speed = spd;
    }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
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
        //private int filter_status;
        //private bool icemaker_status;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", int rid = 0, bool state = false, int temp2 = 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
            temp = temp2;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
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

    class Alarmsystem : device
    {
        //public bool alarm_system_status;
        //public bool arm_status;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", int rid = 0, bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
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

    class Light : device
    {
        private int brightness;

        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", int rid = 0, bool state = false, int bright = 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
            brightness = bright;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; } // closed = false, open = true 

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

    class Motionsensor : device
    {
        private bool motion;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", int rid = 0, bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 

        public bool getMotion()
        {
            //trigger when capture movement
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
    class Thermostat : device
    {
        private int tempCurrent;
        private int humidityCurrent;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", int rid = 0, bool state = false, int temp2= 0, int hum2= 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            roomid = rid;
            status = state;  // open/close, on/off
            tempCurrent = temp2;
            humidityCurrent = hum2;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override int getroomid() { return id; }
        public override void setroomid(int rid2) { roomid = rid2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; } // closed = false, open = true 

        public int getTemp()
        {
            return tempCurrent;
        }
        public void setTemp(int tempDesired)
        {
            tempCurrent = tempDesired;
        }
        public int getHumidity()
        {
            return humidityCurrent;
        }

        public void setHumidity(int humidityDesired)
        {
            humidityCurrent = humidityDesired;
        }
    } /* end class HVAC */
}


