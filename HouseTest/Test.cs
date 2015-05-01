using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using Hats.Time;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HouseTest
{
[TestFixture]
public class Test
{
	protected Process _house;
	[SetUp]
	public void Setup()
	{
		var debug_path = System.Environment.GetEnvironmentVariable("HOUSE_PATH");
		ProcessStartInfo house = new ProcessStartInfo();
		house.FileName = debug_path;
		house.Arguments = "--house_id=house1 --test_scenario=\'{ \"storageLocation\": \"54.152.190.217\", \"serverLocation\": \"http://5574serverapi.azurewebsites.net/\", \"users\": [ { \"Username\":\"User1\", \"UserID\": \"12345\", \"Password\": \"thePassword\", \"Coordinates\": { \"x\":\"1234\", \"y\":\"4321\", \"z\":\"6789\" } }, { \"Username\": \"User2\", \"UserID\": \"67890\", \"Password\": \"secondPassword\", \"Coordinates\": { \"x\":\"1234\", \"y\":\"4321\", \"z\":\"6789\" } } ], \"houses\": [ { \"name\": \"house1\", \"port\": 8081, \"devices\": [ { \"name\": \"light1\", \"class\": \"LightSwitch\", \"type\": \"Simulated\", \"startState\": false }, { \"name\": \"Kitchen Ceiling Fan\", \"class\": \"CeilingFan\", \"type\": \"Simulated\", \"Enabled\": false, \"State\": 0 }, { \"name\": \"thermo\", \"class\": \"Thermostat\", } ], \"rooms\": [ { \"name\": \"Kitchen\", \"dimensions\": { \"x\": 100, \"y\": 200 }, \"roomLevel\": 1, \"doors\": [ { \"x\": 20, \"y\": 200, \"connectingRoom\": 1 } ], \"devices\": [ 1 ] }, { \"name\": \"Family Room\", \"dimensions\": { \"x\": 300, \"y\": 200 }, \"roomLevel\": 1, \"doors\": [ { \"x\": 20, \"y\": 0, \"connectingRoom\": 0 } ], \"devices\": [ 0 ] } ], \"weather\": [ { \"Time\": \"2015-04-08T13:25:21.803833-04:00\", \"Temp\": 50 }, { \"Time\": \"2015-04-08T13:25:21.803833-04:00\", \"Temp\": 30 } ] } ]}\'";
		house.RedirectStandardInput = true;
		house.RedirectStandardOutput = true;
		house.RedirectStandardError = true;
		house.UseShellExecute = false;

		try
		{
			_house = Process.Start(house);
			var resp = _house.StandardOutput.ReadLine();
			if(resp != "OK")
			{
				throw new ArgumentException();
			}
			_house.StandardInput.WriteLine(JsonConvert.SerializeObject(new TimeFrame()));
		}
		catch(Exception ex)
		{
			Assert.IsTrue(false);
		}
		//wait for server to fully spin up
		System.Threading.Thread.Sleep(1000);
	}

	[Test]
	public void QueryTest()
	{
		var client = new RestClient("http://127.0.0.1:8081");

		var query = new RestRequest(Method.GET);
		query.Resource = "api/device";
		query.RequestFormat = DataFormat.Json;
		var resp = client.Execute(query);
		Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
		const String ExpectedResponse = "[{\"Enabled\":false,\"Value\":0.0,\"ID\":0,\"LastUpdate\":\"2015-04-21T13:28:10.5439104\",\"Name\":\"light1\",\"Class\":\"LightSwitch\"},{\"Enabled\":false,\"State\":0,\"ID\":1,\"LastUpdate\":\"2015-04-21T13:28:10.5442048\",\"Name\":\"Kitchen Ceiling Fan\",\"Class\":\"CeilingFan\"},{\"Enabled\":false,\"SetPoint\":0.0,\"Value\":0.0,\"ID\":2,\"LastUpdate\":\"2015-04-21T13:28:10.5442048\",\"Name\":\"thermo\",\"Class\":\"Thermostat\"}]";
		var resp_obj = JArray.Parse(resp.Content);
		var test_obj = JArray.Parse(ExpectedResponse);
		Assert.AreEqual(resp_obj.Count, test_obj.Count);
		foreach(var obj in resp_obj)
		{
			Console.WriteLine(obj.SelectToken("Name").ToString());
		}
	}
}

}
