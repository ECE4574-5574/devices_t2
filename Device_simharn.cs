using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace Simharn_to_DeviceAPI
{
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
        public List<dev_type> dev_types { get; set; }
    }

    public class dev_type
    {
        public Lights Lightss { get; set; }
        public Garagedoor Garagedoors { get; set; }
        public Ceilingfan Ceilingfans { get; set; }
        public Thermostat Thermostats { get; set; }
        public Alarmsystem Alarmsystem { get; set; }
        public Refrigerator Refrigerators { get; set; }
        public Motionsensor Motionsensor { get; set; }
    }

    public class Lights
    {
        public String light_name { get; set; }
        public String light_ID { get; set; }
        public String light_startstate { get; set; }
        public String light_brightness { get; set; }
    }

    public class Garagedoor
    {
        public String gd_name { get; set; }
        public String gd_ID { get; set; }
        public String gd_startstate { get; set; }
    }

    public class Ceilingfan
    {
        public String cf_name { get; set; }
        public String cf_ID { get; set; }
        public String cf_startstate { get; set; }
        public String cf_speed { get; set; }
    }

    public class Thermostat
    {
        public String t_name { get; set; }
        public String t_ID { get; set; }
        public String t_startstate { get; set; }
        public String t_temp { get; set; }
    }

    public class Alarmsystem
    {
        public String a_name { get; set; }
        public String a_ID { get; set; }
        public String a_startstate { get; set; }
    }

    public class Motionsensor
    {
        public String m_name { get; set; }
        public String m_ID { get; set; }
        public String m_startstate { get; set; }
    }

    public class Refrigerator
    {
        public String r_name { get; set; }
        public String r_ID { get; set; }
        public String r_startstate { get; set; }
    }


    class Program : Device
    {
        static void Main()
        {
            string json = Console.In.ReadToEnd();
            //WebClient c = new WebClient();
            //var json = c.DownloadString("simharn_url");
            Houses house = JsonConvert.DeserializeObject<Houses>(json);
            

        }
    }
}
