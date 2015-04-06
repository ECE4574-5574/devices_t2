/*
* Contributor: Pedro Sorto, Kara Dodenhoff, Steven Cho, Danny Mota, Aakruthi Gopisetty, Dong Nan
* RESTful device API
* Json string will be read through the inputstream
* Json implementation will be handled in the devices class
*/
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using NDesk.Options;

namespace House
{
	public class HouseMain
	{
		//initializes and starts thread
		public static int Main(String[] args)
		{
            bool show_help = false;
            Dictionary<string, string> config = new Dictionary<string, string>();
            var optargs = new OptionSet()
            {
                {"t|test_scenario=","JSON blob representing the entire test scenario to run", v => config.Add("test_scenario", v) },
                {"i|house_id=", "Unique identifier for the house to simulate from the scenario", v => config.Add("house_id", v) },
                {"h|help", "Display the help message", v => show_help = v != null },
            };

            List<string> extras;
            try
            {
                extras = optargs.Parse(args);
            }
            catch(OptionException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }

            if(show_help || config.Count == 0)
            {
                PrintHelp(optargs);
            }

            bool valid_sim = true;
            if(!config.ContainsKey("test_scenario"))
            {
                //Console.WriteLine("Received scenario:");
                //Console.WriteLine(config["test_scenario"]);
                valid_sim = false;
            }
            if(config.ContainsKey("house_id"))
            {
                //Console.WriteLine("House ID set to:");
                //Console.WriteLine(config["house_id"]);
                valid_sim = false;
            }

            if(valid_sim)
            {
                Console.WriteLine("OK");
            }

            return 0;
		}

        public static void PrintHelp(OptionSet p)
        {
            Console.WriteLine("Usage: House [Options]");
            Console.WriteLine("Provides the House Interface for a collection of devices.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
	}
}