using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using api;
using Hats.Time;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Diagnostics;

namespace api_tests
{
[TestFixture]
public class APITest
{
	[SetUp]
	public void Init()
	{
		System.Diagnostics.Trace.Listeners.Add(new ConsoleTraceListener());
	}

	[Test]
	public void TestServerInput()
	{
		var input = new ServerInput("http://serverapi1.azurewebsites.net");
		var device1 = new LightSwitch(input, null, null)
		{
			ID = new FullID(3, 1, 2)
		};
		var device2 = new AlarmSystem(input, null, null)
		{
			ID = new FullID(4, 3, 7)
		};
		var device3 = new CeilingFan(input, null, null)
		{
			ID = new FullID(1, 3, 6)
		};
		var device4 = new GarageDoor(input, null, null)
		{
			ID = new FullID(2, 4, 3)
		};
		var device5 = new LightSwitch(input, null, null)
		{
			ID = new FullID(1, 1, 1)
		};
		var device6 = new Thermostat(input, null, null)
		{
			ID = new FullID(0, 0, 0)
		};
		var response1 = input.read(device1);
		var response2 = input.read(device2);
		var response3 = input.read(device3);
		var response4 = input.read(device4);
		var response5 = input.read(device5);
		Assert.AreEqual(true, response1);
		Assert.AreEqual(true, response2);
		Assert.AreEqual(true, response3);
		Assert.AreEqual(true, response4);
		Assert.AreEqual(true, response5);

	}

	[Test]
	public void TestEnumerateDevices()
	{
		Interfaces inter = new Interfaces("http://serverapi1.azurewebsites.net");
		ulong houseID = 4;
		List<string> response = inter.enumerateDevices(houseID);
	}

	[Test]
	public void TestLightSerialization()
	{
		string test = "1.0";
		var light = new Light()
		{
			Brightness = 1.0
		};

		var test_state = JsonConvert.DeserializeObject<Light>("");
		Assert.AreEqual(test_state, null);
		var state = JsonConvert.DeserializeObject<Light>(test);
		Assert.AreNotEqual(state, null);
		Assert.AreEqual(state.Brightness, light.Brightness);

		var output = JsonConvert.SerializeObject(light);
		Assert.AreNotEqual(output, null);
		Assert.AreEqual(test, output);
	}

	[Test]
	public void TestTemperatureSerialization()
	{
		string invalid_string = "";
		var invalid_object = JsonConvert.DeserializeObject<Temperature>(invalid_string);
		Assert.AreEqual(invalid_object, null);

		double temp = 37.2;
		string simple_string = temp.ToString();

		var valid_object = JsonConvert.DeserializeObject<Temperature>(simple_string);
		Assert.AreNotEqual(valid_object, null);
		Assert.AreEqual(temp, valid_object.C);

		var valid_blob = JsonConvert.SerializeObject(valid_object);
		Assert.AreNotEqual(valid_blob, null);
		Assert.AreEqual(valid_blob, simple_string);
	}

	[Test]
	public void TestDeviceDeserialization()
	{
		string invalid_string = "";

		var invalid_object = Interfaces.DeserializeDevice(invalid_string, null, null, null);
		Assert.AreEqual(invalid_object, null);

		string valid_string = "{class: \"LightSwitch\", enabled: true, Brightness: 1.0}";
		var valid_switch = Interfaces.DeserializeDevice(valid_string, null, null, null);

		Assert.IsInstanceOf<LightSwitch>(valid_switch);
		var ls = valid_switch as LightSwitch;
		Assert.IsTrue(ls.Enabled);
		Assert.AreEqual(ls.Value.Brightness, 1.0);
	}

	[Test]
	public void TestDeviceTimeInit()
	{

		var input = new ServerInput("http://serverapi1.azurewebsites.net");
		var device = new LightSwitch(input, null, null);
		Assert.AreEqual(device.LastUpdate, DateTime.MinValue);
	}

	[Test()]
	public void TestDeviceTimeUpdate()
	{
		var input = new ServerInput("http://serverapi1.azurewebsites.net");
		var device = new LightSwitch(input, null, null);
		device.LastUpdate = DateTime.Now.ToUniversalTime();
		Assert.AreNotEqual(device.LastUpdate, DateTime.MinValue);
	}

	[Test]
	public void TestDeviceCapabilityQueries()
	{
		List<Device> devices = new List<Device>()
		{
			new AlarmSystem(null, null, null),
			new CeilingFan(null, null, null),
			new GarageDoor(null, null, null),
			new LightSwitch(null, null, null),
			new Thermostat(null, null, null)
		};

		foreach(var device in devices)
		{
			Assert.IsTrue(device as IEnableable != null);
		}
		List<bool> IsTempSetPoint = new List<bool>
		{
			false,
			false,
			false,
			false,
			true
		};

		var comparison = devices.Zip(IsTempSetPoint, (ii, jj) => new {Device = ii, Result = jj});

		foreach(var set in comparison)
		{
			var intf = set.Device as ISetPointable<Temperature>;
			Assert.AreEqual(intf != null, set.Result);
		}

		List<bool> IsTempReadable = new List<bool>
		{
			false,
			false,
			false,
			false,
			true
		};

		comparison = devices.Zip(IsTempReadable, (ii, jj) => new {Device = ii, Result = jj});
		foreach(var set in comparison)
		{
			Assert.AreEqual(set.Device as IReadable<Temperature> != null, set.Result);
		}
		List<bool> IsDiscrete = new List<bool>
		{
			false,
			true,
			false,
			false,
			false
		};

		comparison = devices.Zip(IsDiscrete, (ii, jj) => new {Device = ii, Result = jj});
		foreach(var set in comparison)
		{
			Assert.AreEqual(set.Device as IDiscreteSetting != null, set.Result);
		}
		List<bool> IsLightReadable = new List<bool>
		{
			false,
			false,
			false,
			true,
			false
		};

		comparison = devices.Zip(IsLightReadable, (ii, jj) => new {Device = ii, Result = jj});
		foreach(var set in comparison)
		{
			Assert.AreEqual(set.Device as IReadable<Light> != null, set.Result);
		}
	}

	[Test]
	[ExpectedException(typeof(ArgumentNullException))]
	public void NullHouseOutputURL()
	{
		var testHO = new HouseOutput(null, null);
	}

	[Test]
	[ExpectedException(typeof(ArgumentException))]
	public void BadHouseOutputURL()
	{
		var testHO = new HouseOutput("", "");
	}

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullTestHouseInput()
    {
        var testHI = new HouseInput(null, null);
    }

	[Test]
	public void TestServerOutput()
	{
		//Test valid URL and Device
		//	to view correct POST:
		//		go to http://postcatcher.in/ and click "Start testing your POST requests now"
		//		copy the URL directly after "Content-Type: application/json" and replace the
		//		URL below (http://postcatcher.in/catchers/5536e135f9562d0300003e57) with the
		//		URL from postcatcher

		const string url = "http://postcatcher.in/catchers/55439a9f51155a03000005a5";
		var testSO = new ServerOutput(url);

		Assert.IsNotNull(testSO);
		Assert.IsTrue(url == testSO.getServerURL());

		testSO.write(new GarageDoor(null, null, null));

		Assert.IsNotNull(Encoding.UTF8.GetString(testSO.getData(), 0, testSO.getData().Length));
		Assert.IsNull(testSO.getURLException());

		//Test null URL
		const string nullURL = null;
		var testNullURLSO = new ServerOutput(nullURL);

		testNullURLSO.write(new AlarmSystem(null, null, null));

		Assert.IsNotNull(testNullURLSO.getURLException());
		Assert.IsNull(testNullURLSO.getStreamException());
		Assert.IsNull(testNullURLSO.getRequestException());

		//Test bad URL
		const string badURL = "http://bkicia";
		var testBadURLSO = new ServerOutput(badURL);

		testBadURLSO.write(new GarageDoor(null, null, null));

		Assert.IsNull(testBadURLSO.getURLException());
		//Assert.IsNotNull(testBadURLSO.getStreamException());
		Assert.IsNull(testBadURLSO.getRequestException());
	}
	[Test()]
	public void TestRegisterAndGetDevice()
	{
		const string url = "http://postcatcher.in/catchers/55439a9f51155a03000005a5";
		Interfaces inter = new Interfaces(url);
		Assert.IsNotNull(inter);

		ulong houseID = 4;
		string name = "light1";
		string info = "";
		ulong roomID = 3;
		//List<Device> response = inter.getDevices(houseID);
		Device response1 = inter.registerDevice(name, houseID, info, roomID);
		//List<Device> response2 = inter.getDevices(houseID, roomID);
		//List<string> response = inter.enumerateDevices(houseID);
	}

	[Test]
	public void TestUpdateDevice()
	{
		var id = new FullID(0, 0, 1);
		var th1 = new Thermostat(null, null, null);
		th1.Enabled = true;
		th1.SetPoint = 100;
		th1.ID = id;

		var th2 = new Thermostat(null, null, null);
		th2.Enabled = false;
		th2.SetPoint = 0;
		th2.ID = id;

		Assert.AreNotEqual(th1.Enabled, th2.Enabled);
		Assert.AreNotEqual(th1.SetPoint.C, th2.SetPoint.C);

		Assert.IsTrue(Interfaces.UpdateDevice(th2, th1));
		Assert.AreEqual(th1.Enabled, th2.Enabled);
		Assert.AreEqual(th1.SetPoint, th2.SetPoint);

		Assert.IsFalse(Interfaces.UpdateDevice(th2, th1));
	}

	[Test]
	public void VerifyDeviceBuilders()
	{
		const string valid_string = "{class: \"LightSwitch\", enabled: true, Brightness: 1.0}";
		var intf = new Interfaces("http://localhost");

		var dev = intf.CreateDevice(valid_string, null);
		Assert.IsNotNull(dev);
		Assert.AreEqual(dev.Input.GetType(), typeof(ServerInput));
		Assert.AreEqual(dev.Output.GetType(), typeof(ServerOutput));
	}
}
}
