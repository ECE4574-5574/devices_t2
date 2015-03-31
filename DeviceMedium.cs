using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
//using MySql.Data.MySqlClient; 
namespace ASSIGN5TEAM2
{
    class DeviceMedium : device
    {
        static void Main(string[] args)
        {
            //string json = Console.In.ReadToEnd();
            //WebClient c = new WebClient();
            //var json = c.DownloadString("simharn_url");
            //Houses house = JsonConvert.DeserializeObject<Houses>(json);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";


            /*
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
            */
            string answer, output, push;
            string devid, devName, type, roomid, state; //garage, light, thermostat, alarmsystem, motionsensor, refrigerator, etc 
            string brightness, speed, temp;
            int conid, conroom, conbri = 0, conspeed = 0, contemp = 0;
            bool status;
            Console.WriteLine("Enter dev_types:");
            type = Console.ReadLine();


            switch (type)
            {
                // assuming no device id we need request (output)
                case "light":
                    Light boo2 = new Light();
                    Console.WriteLine("Enter devid:");
                    devid = Console.ReadLine();
                    conid = Convert.ToInt32(devid);
                    Console.WriteLine("Enter dev_name:");
                    devName = Console.ReadLine();
                    Console.WriteLine("Enter roomid:");
                    roomid = Console.ReadLine();
                    conroom = Convert.ToInt32(roomid);
                    Console.WriteLine("Enter status:");
                    state = Console.ReadLine();
                    if (state == "true")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    Console.WriteLine("Enter brightness:");
                    brightness = Console.ReadLine();
                    conbri = Convert.ToInt32(brightness);

                    output = JsonConvert.SerializeObject(boo2);
                    boo2.addDevice(conid, devName, type, conroom, status, conbri);
                    push = JsonConvert.SerializeObject(boo2);
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"devid\":\"test\"," +
                                      "\"devname\":\"bla\"," +
                                      "\"devtype\":\"bla2\"," +
                                      "\"roomid\":\"bla3\"." +
                                      "\"status\":\"bla4\"," +
                                      "\"brightness\":\"bla5\"}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                    Console.WriteLine(boo2.getid());
                    Console.WriteLine(boo2.getDeviceType());
                    Console.WriteLine(boo2.getStatus());
                    Console.WriteLine(boo2.getBrightness());
                    break;
                case "ceilingfan":
                    Ceilingfan boo3 = new Ceilingfan();
                    output = JsonConvert.SerializeObject(boo3);
                    // boo3.addDevice(output, devName, type, conroom, boolVal);
                    Console.WriteLine(boo3.getid());
                    Console.WriteLine(boo3.getDeviceType());
                    Console.WriteLine(boo3.getStatus());
                    break;
                case "garage":
                    Garagedoor boo = new Garagedoor();
                    output = JsonConvert.SerializeObject(boo);
                    //get deviceid server api
                    // boo.addDevice(output, devName, type, conroom, boolVal);

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
