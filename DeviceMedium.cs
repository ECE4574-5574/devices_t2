using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient; 
namespace ASSIGN5TEAM2
{
    class DeviceMedium : device
    {
        static void Main(string[] args)
        {

            ArrayList deviceList = new ArrayList();

            //Read from Server
            /*
            public List<string>[] getDeviceList()
            {
            string query = "select * from cart c inner join product p ON p.asin = c.asin";
            List<string>[] list = new List<string>[1];
		    list[0] = new List<string>();

		    if (OpenConnection()) //Open MYSQL connection
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        list[0].Add(reader["devType"].ToString());
                        // Add function:  add(id, devType, status)
                    }
                }
                catch (MySqlException e)
                {
                    CloseConnection(); //Close MYSQL Connection
                    return null;
                }
            }
            CloseConnection(); //Close MYSQL connection
            return list;
            } */


            // testing garage door object to get ID, Device Name, Room ID, and Status
            GarageDoor boo = new GarageDoor();

            boo.addDevice(2, "Garage1", false);
            Console.WriteLine(boo.getid());
            Console.WriteLine(boo.getDeviceType());
            Console.WriteLine(boo.getStatus());

            deviceList.Add(boo);

            sprinkler boo2 = new sprinkler();

            boo2.addDevice(4, "Sprinkler", false);
            Console.WriteLine(boo2.getid());
            Console.WriteLine(boo2.getDeviceType());
            Console.WriteLine(boo2.getStatus());
            Console.WriteLine("change status of sprinkler...");
            boo2.turnOn();
            Console.WriteLine(boo2.getStatus());
            deviceList.Add(boo2);

            Console.WriteLine("Testing deviceList");
            // Display the values.
            //
            foreach (device i in deviceList)
            {
                Console.WriteLine("ID: {0} \n" +
                                  "Device: '{1}' \n" +
                                  "Status: {2} \n",
                                  i.getid(), i.getDeviceType(), i.getStatus());
            }

            

        }
    }
}
