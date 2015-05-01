using System;
using api;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace api_tests
{
[TestFixture]
public class APITest
{
	[SetUp]
	public void Init()
	{
	}

	[Test]
	public void TestServerInput()
	{
		var input = new ServerInput("http://serverapi1.azurewebsites.net/");
		//  var device = new LightSwitch(input, null, null);
		//var response = input.read(device);
		var dev = new LightSwitch(null, null, null);
		var return_value = input.read(dev);
		Assert.IsTrue(return_value);
	}

	[Test]
	public void TestLightSerialization()
	{
		string test = "1.0";
		var light = new Light() {
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

	[Test()]
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
		List<Device> devices = new List<Device>() {
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
		List<bool> IsTempSetPoint = new List<bool> {
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

		List<bool> IsTempReadable = new List<bool> {
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
		List<bool> IsDiscrete = new List<bool> {
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
		List<bool> IsLightReadable = new List<bool> {
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

	[Test()]
	public void TestDeviceVars()
	{
		List<Device> devices = new List<Device>() {
			new AlarmSystem(null, null, null),
			new CeilingFan(null, null, null),
			new GarageDoor(null, null, null),
			new LightSwitch(null, null, null),
			new Thermostat(null, null, null)
		};

		foreach(var device in devices)
		{
			Assert.IsTrue(device.ID.HouseID == 0 &&
			device.ID.RoomID == 0 &&
			device.ID.DeviceID == 0);
		}

		foreach(var device in devices)
		{
			device.ID.HouseID = 1;
			device.ID.RoomID = 2;
			device.ID.DeviceID = 3;
		}

		foreach(var device in devices)
		{
			Assert.IsTrue(device.ID.HouseID == 1 &&
			device.ID.RoomID == 2 &&
			device.ID.DeviceID == 3);
		}

		foreach(var device in devices)
		{
			Assert.IsTrue(device.Name.Equals(""));
		}

		foreach(var device in devices)
		{
			device.Name = "NON-EMPTY STRING";
		}

		foreach(var device in devices)
		{
			Assert.IsTrue(device.Name.Equals("NON-EMPTY STRING"));
		}

	}
}

}
