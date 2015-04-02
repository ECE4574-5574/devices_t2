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
        protected bool status;

        protected string [] parsed;
        protected device(int id2 = 0, string devName = "Nameless", string devType = "Type" , bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
        }
        
        public virtual void addDevice(int id, string devType, bool status) { ;}
        public virtual int getid() { return id; }
        public virtual void setid(int id2) { id = id2; }
        public virtual string getDeviceName() { return deviceType; }
        public virtual void setDeviceName(string devName) { deviceName = devName; }
        public virtual string getDeviceType() { return deviceType; }
        public virtual void setDeviceType(string devType) { deviceType = devType; }
        public virtual bool getStatus() { return status; }
        public virtual void setStatus(bool state) { status = state; }
      
    }
    public class Garagedoor : device
    {
        // Attributes
      
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type" , bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // true/false, open/close, on/off
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
        // get pull request
        public String gd_name { get; set; }
        public String gd_ID { get; set; }
        public String gd_startstate { get; set; }
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
    public class Ceilingfan : device
    {
        // Attributes

        public int speed;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type" , bool state = false, int spd = 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
            speed = spd;
    }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
        //get/pull request
        public String cf_name { get; set; }
        public String cf_ID { get; set; }
        public String cf_startstate { get; set; }
        public String cf_speed { get; set; }

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

    public class Refrigerator : device
    {
        // Attributes
        private int temp; // temperature
        //private int filter_status;
        //private bool icemaker_status;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", bool state = false, int temp2 = 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
            temp = temp2;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
        // get/pull request
        public String r_name { get; set; }
        public String r_ID { get; set; }
        public String r_startstate { get; set; }
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

    public class Alarmsystem : device
    {
        //public bool alarm_system_status;
        //public bool arm_status;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", bool state = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }
        // pull/get request
        public String a_name { get; set; }
        public String a_ID { get; set; }
        public String a_startstate { get; set; }
        public void turnOn()
        {
            //Console.WriteLine("Alarm is on.");
            status = true;
        }

        public void turnOff()
        {
            //Console.WriteLine("Alarm is off.");
            status = false;
        }
    } /* end class Alarm System */

    public class Light : device
    {
        private int brightness;

        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", bool state = false, int bright = 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
            brightness = bright;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; } // closed = false, open = true 
        //get/pull request
        public String light_name { get; set; }
        public String light_ID { get; set; }
        public String light_startstate { get; set; }
        public String light_brightness { get; set; }
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

    public class Motionsensor : device
    {
        private bool motion = false;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", bool state = false, bool move = false)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
            motion = move;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; }  // closed = false, open = true 
        // get/pull request
        public String m_name { get; set; }
        public String m_ID { get; set; }
        public String m_startstate { get; set; }
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
    public class Thermostat : device
    {
        private int tempCurrent;
        //private int humidityCurrent;
        public void addDevice(int id2 = 0, string devName = "Nameless", string devType = "Type", bool state = false, int temp2= 0)
        {
            id = id2;
            deviceName = devName;
            deviceType = devType;
            status = state;  // open/close, on/off
            tempCurrent = temp2;
        }
        public override int getid() { return id; }
        public override void setid(int id2) { id = id2; }
        public override string getDeviceName() { return deviceType; }
        public override void setDeviceName(string devName) { deviceName = devName; }
        public override string getDeviceType() { return deviceType; }
        public override void setDeviceType(string devType) { deviceType = devType; }
        public override bool getStatus() { return status; } // closed = false, open = true 
        //get/pull request
        public String t_name { get; set; }
        public String t_ID { get; set; }
        public String t_startstate { get; set; }
        public String t_temp { get; set; }
        public int getTemp()
        {
            return tempCurrent;
        }
        public void setTemp(int tempDesired)
        {
            tempCurrent = tempDesired;
        }
    } /* end class thermostat */
    public class Houses
    {
        public List<Rooms> Roomss { get; set; }
    }

    public class Rooms
    {
        public List<RoomID> RoomIDs { get; set; }
    }

    public class RoomID
    {
        public string Type { get; set; }
        public dimensions dimensionss { get; set; }
        public string roomlevel { get; set; }
        public List<connectingrooms> connectingroomss { get; set; }
        public string roomname { get; set; }
        public List<devices> devicess { get; set; }
    }

    public class dimensions
    {
        public int width { get; set; }
        public int length { get; set; }
    }

    public class connectingrooms
    {

    }

    public class devices
    {
        public List<string> dev_types { get; set; }
    }

    public class dev_type
    {
        public string Lights { get; set; }
        public string Garagedoors { get; set; }
        public string Ceilingfans { get; set; }
        public string Thermostats { get; set; }
        public string Alarmsystems { get; set; }
        public string Refrigerators { get; set; }
        public string Motionsensors { get; set; }
    }


}




