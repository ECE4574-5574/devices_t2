using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
//using MySql.Data.MySqlClient; 
namespace ASSIGN5TEAM2
{
    class DeviceMedium : DeviceAPI
    {
        static void Main()
        {
     
            //string json = Console.In.ReadToEnd();
            //WebClient c = new WebClient();
            //var json = c.DownloadString("simharn_url");
            //Houses house = JsonConvert.DeserializeObject<Houses>(json);
            
           // devices deviceJ = JsonConvert.DeserializeObject<devices>(json);
            //bool run = true; while loop testing
            string answer, output;


            string devid, devName, type //garage, light, thermostat, alarmsystem, motionsensor, refrigerator, etc 
                , roomid, state;
            string brightness, speed, temp;
            int conid, conroom , conbri = 0, conspeed = 0, contemp = 0;
            bool boolVal;

            
            //Console.WriteLine("Enter devid:");
            

            
            type = dev_types;
            if (type == Lightss)
            {

                //devid = //deserialize(;
                conid = Convert.ToInt32(devid);
                devName = house.light_name;
                //roomid = house.light_room;
                //conroom = Convert.ToInt32(roomid);
                state = house.light_startstate;
                brightness = house.light_brightness;
                conbri = Convert.ToInt32(brightness);
            }
            else if (type == Ceilingfans)
            {
                //devid, 
                devName = house.cf_name;
                //roomid = house.dev_id;
                //conroom = Convert.ToInt32(roomid);
                state = house.cf_startstate;
                speed = cf_speed;
                conspeed = Convert.ToInt32(speed);
            }
            else if (type == "thermostat")
            {
                devName = house.t_name;
                //roomid = house.t_room;
                //conroom = Convert.ToInt32(roomid);
                state = house.t_startstate;
                temp = house.t_temp;
                contemp = Convert.ToInt32(temp);
            }
            else
            {

            }

            switch (type)
            {
                
                case "light":
                    Lights boo2 = new Lights();
                    output = JsonConvert.SerializeObject(boo2);
                    boo2.addDevice(conid, devName, type, conroom, boolVal, conbri);
                    Console.WriteLine(boo2.getid());
                    Console.WriteLine(boo2.getDeviceType());
                    Console.WriteLine(boo2.getStatus());
                    break;
                case "ceilingfan":
                    Ceilingfan boo3 = new Ceilingfan();
                    output = JsonConvert.SerializeObject(boo3);
                    boo3.addDevice(conid, devName, type, conroom, boolVal);
                    Console.WriteLine(boo3.getid());
                    Console.WriteLine(boo3.getDeviceType());
                    Console.WriteLine(boo3.getStatus());
                    break;
                case "garage":
                    Garagedoor boo = new Garagedoor();
                    output = JsonConvert.SerializeObject(boo);
                    boo.addDevice(conid, devName, type, conroom, boolVal);
                    Console.WriteLine(boo.getid());
                    Console.WriteLine(boo.getDeviceType());
                    Console.WriteLine(boo.getStatus());
                    break;     

                default:
                    break;
            }
           
        }


    }

}
