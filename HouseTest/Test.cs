﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using api;
using api.Converters;
using Hats.Time;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Reflection;

namespace HouseTest
{
[TestFixture]
public class Test
{
	protected Process _house;
	protected RestClient _client;
	protected TimeFrame _frame;
	[SetUp]
	public void Setup()
	{
		var debug_path = System.Environment.GetEnvironmentVariable("HOUSE_PATH");
		ProcessStartInfo house = new ProcessStartInfo();
		house.FileName = debug_path;
		house.Arguments = "--house_id=0 --test_scenario='{ \"storageLocation\": \"54.152.190.217\", \"serverLocation\": \"http://5574serverapi.azurewebsites.net/\", \"users\": [ { \"Username\":\"User1\", \"UserID\": \"12345\", \"Password\": \"thePassword\", \"Coordinates\": { \"x\":\"1234\", \"y\":\"4321\", \"z\":\"6789\" } }, { \"Username\": \"User2\", \"UserID\": \"67890\", \"Password\": \"secondPassword\", \"Coordinates\": { \"x\":\"1234\", \"y\":\"4321\", \"z\":\"6789\" } } ], \"houses\": [ { \"name\": \"house1\", \"port\": 8081, \"id\": 0, \"devices\": [ { \"name\": \"light1\", \"class\": \"LightSwitch\", \"type\": \"Simulated\", \"startState\": false }, { \"name\": \"Kitchen Ceiling Fan\", \"class\": \"CeilingFan\", \"type\": \"Simulated\", \"Enabled\": false, \"State\": 0 }, { \"name\": \"HVAC\", \"class\": \"Thermostat\", \"Enabled\": false, \"SetPoint\": 26.6 } ], \"rooms\": [ { \"name\": \"Kitchen\", \"dimensions\": { \"x\": 100, \"y\": 200 }, \"roomLevel\": 1, \"doors\": [ { \"x\": 20, \"y\": 200, \"connectingRoom\": 1 } ], \"devices\": [ 1 ] }, { \"name\": \"Family Room\", \"dimensions\": { \"x\": 300, \"y\": 200 }, \"roomLevel\": 1, \"doors\": [ { \"x\": 20, \"y\": 0, \"connectingRoom\": 0 } ], \"devices\": [ 0 ] } ], \"weather\": [ { \"Time\": \"2015-04-08T13:25:21.000000-04:00\", \"Temp\": 50 }, { \"Time\": \"2015-04-08T13:28:24.000000-04:00\", \"Temp\": 30 } ] } ]}'";
		house.RedirectStandardInput = true;
		house.RedirectStandardOutput = true;
		house.RedirectStandardError = true;
		house.UseShellExecute = false;

		_frame = JsonConvert.DeserializeObject<TimeFrame>("{\"SimEpoch\": \"2015-04-08T13:25:20.0-04:00\"}");

		try
		{
			_house = Process.Start(house);
			var resp = _house.StandardOutput.ReadLine();
			if(resp != "OK")
			{
				throw new ArgumentException();
			}
			_house.StandardInput.WriteLine(JsonConvert.SerializeObject(_frame));
		}
		catch(Exception ex)
		{
			Assert.IsTrue(false);
		}
		//wait for server to fully spin up
		System.Threading.Thread.Sleep(1000);
		_client = new RestClient("http://127.0.0.1:8081");
	}

	[TearDown]
	public void TearDown()
	{
		_house.Kill();
	}

	[Test]
	public void QueryTest()
	{
		var query = new RestRequest(Method.GET);
		query.Resource = "api/device";
		query.RequestFormat = DataFormat.Json;
		var resp = _client.Execute(query);
		Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
		const String ExpectedResponse = "[{\"Enabled\":false,\"Value\":0.0,\"ID\":0,\"LastUpdate\":\"2015-04-21T13:28:10.5439104\",\"Name\":\"light1\",\"Class\":\"LightSwitch\"},{\"Enabled\":false,\"State\":0,\"ID\":1,\"LastUpdate\":\"2015-04-21T13:28:10.5442048\",\"Name\":\"Kitchen Ceiling Fan\",\"Class\":\"CeilingFan\"},{\"Enabled\":false,\"SetPoint\":0.0,\"Value\":0.0,\"ID\":2,\"LastUpdate\":\"2015-04-21T13:28:10.5442048\",\"Name\":\"HVAC\",\"Class\":\"Thermostat\"}]";
		Assert.IsTrue(resp.Content.Length > 0);
		var resp_obj = JArray.Parse(resp.Content);
		var test_obj = JArray.Parse(ExpectedResponse);
		Assert.AreEqual(resp_obj.Count, test_obj.Count);

		foreach(var obj in resp_obj)
		{
			var resp_dev = Interfaces.DeserializeDevice(obj.ToString(), null, null, _frame);
			foreach(var exp_obj in test_obj)
			{
				var exp_dev = Interfaces.DeserializeDevice(exp_obj.ToString(), null, null, _frame);
				if(resp_dev.ID.DeviceID == exp_dev.ID.DeviceID)
				{
					Assert.AreEqual(resp_dev.ID.DeviceID, exp_dev.ID.DeviceID);
					Assert.IsTrue(resp_dev.UpdateOk);
					Assert.AreEqual(exp_dev.Name, resp_dev.Name);
					Assert.AreEqual(exp_dev.Class, resp_dev.Class);
					break;
				}
			}
		}
	}

	[Test]
	public void SetLightTest()
	{
		var id = new FullID(0, 0, 0);
		const string HouseString = "{\"house_url\":\"http://127.0.0.1:8081\"}";
		const string DeviceString = "{\"ID\": 0, \"Class\": \"LightSwitch\"}";
		var dev_out = ServerSideAPI.CreateDevice(id, HouseString, DeviceString, _frame);

		Assert.IsNotNull(dev_out);
		var ls = (LightSwitch)dev_out;

		Assert.IsNotNull(ls);
		ls.Enabled = true;

		Assert.IsTrue(ls.Enabled);
		Assert.IsTrue(ls.UpdateOk);
		Assert.AreEqual(ls.Value.Brightness, 1.0);
	}

	[Test]
	public void TestHouseInput()
	{
		const string HouseString = "{\"house_url\":\"http://127.0.0.1:8081\"}";
		const string DeviceString = "{\"ID\": 0, \"Class\": \"LightSwitch\"}";
		var inp = new HouseInput(HouseString, DeviceString);
		var ls = new LightSwitch(inp, null, _frame);
		Assert.IsTrue(ls.update());
		Assert.AreEqual(ls.Enabled, false);
	}

	[Test]
	public void SetThermoTest()
	{
		var id = new FullID(0, 0, 2);
		const string HouseString = "{\"house_url\":\"http://127.0.0.1:8081\"}";
		const string DeviceString = "{\"ID\": 2, \"Class\": \"Thermostat\"}";
		var dev_out = ServerSideAPI.CreateDevice(id, HouseString, DeviceString, _frame);

		var therm = (Thermostat)dev_out;
		Assert.IsNotNull(therm);
		therm.Enabled = true;
		Assert.IsTrue(therm.UpdateOk);
		therm.SetPoint = 45.0;

		therm.update();
		Assert.IsTrue(therm.UpdateOk);
		Assert.AreEqual(therm.Enabled, true);
		Assert.AreEqual(therm.SetPoint.C, 45.0);
	}

	[Test]
	public void TestSimTemps()
	{
		var old_dev = GetThermostat();
		Thread.Sleep(150);
		for(int ii = 0; ii < 10; ++ii)
		{
			var new_dev = GetThermostat();
			//Temperatures they are a-changin'
			Assert.AreNotEqual(old_dev.Value.C, new_dev.Value.C);
			old_dev = new_dev;
			Thread.Sleep(150);
		}
	}

	public Thermostat GetThermostat()
	{
		var client = new HttpClient();
		client.BaseAddress = new Uri("http://127.0.0.1:8081");
		var check = client.GetAsync("api/device/2");
		check.Wait();
		if(!check.Result.IsSuccessStatusCode)
		{
			return null;
		}

		var resp = check.Result.Content.ReadAsStringAsync();
		resp.Wait();

		return (Thermostat)Interfaces.DeserializeDevice(resp.Result, null, null, _frame);
	}
}

}
