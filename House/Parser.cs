// accepts json strings and parses in order to create the respective device objects.
// contributers: Steven Cho, Nan Dong, Aakruthi Gopisetty

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace ASSIGN5TEAM2
{
    class DeviceMedium : device
    {
        static void ParserMain(string[] args)
        {

            //string output, push;
            string devid;
            string devName, type, state; //garage, light, thermostat, alarmsystem, motionsensor, refrigerator, etc 
            string brightness, speed, temp;
            int conid, conbri = 0, conspeed = 0, contemp = 0;
            bool status;

            string input = Console.In.ReadToEnd();
            //WebClient c = new WebClient();
            //var json = c.DownloadString("simharn_url");
            Houses house = JsonConvert.DeserializeObject<Houses>(input);

            string json;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://5574serverapi.azurewebsites.net/");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            //var httpResponse;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            Garagedoor boo;
            Light boo2;
            Ceilingfan boo3;
            Thermostat boo4;
            Alarmsystem boo5;
            Refrigerator boo7;
            Motionsensor boo8;
            devices dlist = new devices();
            type = dlist.dev_types.ToString();
            if (type == "Lightss")
            {
                boo2 = new Light();
                devName = boo2.light_name;
                state = boo2.light_startstate;
                brightness = boo2.light_brightness;
                conbri = Convert.ToInt32(brightness);
                if (state == "true")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    devid = streamReader.ReadToEnd();
                }
                conid = Convert.ToInt32(devid);
                conbri = Convert.ToInt32(brightness);
                boo2.addDevice(conid, devName, type, status, conbri);
                json = JsonConvert.SerializeObject(boo2);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"devid\":\"bla0\"," +
                                  "\"devname\":\"bla1\"," +
                                  "\"devtype\":\"bla2\"," +
                                  "\"status\":\"bla4\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else if (type == "Ceilingfans")
            {
                boo3 = new Ceilingfan();
                devName = boo3.cf_name;
                state = boo3.cf_startstate;
                speed = boo3.cf_speed;
                conspeed = Convert.ToInt32(speed);
                if (state == "true")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    devid = streamReader.ReadToEnd();
                }
                conid = Convert.ToInt32(devid);
                boo3.addDevice(conid, devName, type, status);
                json = JsonConvert.SerializeObject(boo3);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"devid\":\"bla0\"," +
                                  "\"devname\":\"bla1\"," +
                                  "\"devtype\":\"bla2\"," +
                                  "\"status\":\"bla4\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else if (type == "Thermostats")
            {
                boo4 = new Thermostat();
                devName = boo4.t_name;
                state = boo4.t_startstate;
                temp = boo4.t_temp;
                contemp = Convert.ToInt32(temp);

            }
            else if (type == "Garagedoor")
            {
                boo = new Garagedoor();
                devName = boo.gd_name;
                state = boo.gd_startstate;
                if (state == "true")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    devid = streamReader.ReadToEnd();
                }
                conid = Convert.ToInt32(devid);
                boo.addDevice(conid, devName, type, status);
                json = JsonConvert.SerializeObject(boo);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"devid\":\"bla0\"," +
                                  "\"devname\":\"bla1\"," +
                                  "\"devtype\":\"bla2\"," +
                                  "\"status\":\"bla4\"," +
                                  "\"brightness\":\"bla5\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

            }
            else if (type == "Alarmsystems")
            {
                boo5 = new Alarmsystem();
                devName = boo5.a_name;
                state = boo5.a_startstate;
            }
            else if (type == "Motionsensors")
            {
                boo8 = new Motionsensor();
                devName = boo8.m_name;
                state = boo8.m_startstate;
                if (state == "true")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    devid = streamReader.ReadToEnd();
                }
                conid = Convert.ToInt32(devid);
                boo8.addDevice(conid, devName, type, status);
                json = JsonConvert.SerializeObject(boo8);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"devid\":\"bla0\"," +
                                  "\"devname\":\"bla1\"," +
                                  "\"devtype\":\"bla2\"," +
                                  "\"status\":\"bla4\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else if (type == "Refrigerators")
            {
                boo7 = new Refrigerator();
                devName = boo7.r_name;
                state = boo7.r_startstate;
                if (state == "true")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    devid = streamReader.ReadToEnd();
                }
                conid = Convert.ToInt32(devid);
                boo7.addDevice(conid, devName, type, status);
                json = JsonConvert.SerializeObject(boo7);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"devid\":\"bla0\"," +
                                  "\"devname\":\"bla1\"," +
                                  "\"devtype\":\"bla2\"," +
                                  "\"status\":\"bla4\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

        }
    }
}
