using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using api;
using Hats.Time;
using Newtonsoft.Json;
using NUnit.Framework;

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
        var input = new ServerInput();
        var device = new LightSwitch(input, null, null);
        var response = input.read(device);
        Assert.AreEqual(true, response);
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

    [Test()]
    public void TestDeviceTimeInit()
    {
        
        var input = new ServerInput();
        var device = new LightSwitch(input, null, null);
        Assert.AreEqual(device.LastUpdate, DateTime.MinValue);
    }

    [Test()]
    public void TestDeviceTimeUpdate()
    {
        var input = new ServerInput();
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

	[Test()]
	public void TestHouseOutput()
	{
		//Test valid URL and Device
		//	to view correct POST:
		//		go to http://postcatcher.in/ and click "Start testing your POST requests now"
		//		copy the URL directly after "Content-Type: application/json" and replace the
		//		URL below (http://postcatcher.in/catchers/5536e135f9562d0300003e57) with the
		//		URL from postcatcher

		const string url = "http://postcatcher.in/catchers/5536e135f9562d0300003e57";
		var testHO = new HouseOutput(url);

		Assert.IsNotNull(testHO);
		Assert.IsTrue(url == testHO.getURL());

		testHO.write(new AlarmSystem(null, null, null));

		Assert.IsTrue(testHO.getJSON() == Encoding.UTF8.GetString(testHO.getData(), 0, testHO.getData().Length));
		Assert.IsNull(testHO.getURLException());

		//Test null URL
		const string nullURL = null;
		var testNullURLHO = new HouseOutput(nullURL);

		testNullURLHO.write(new AlarmSystem(null, null, null));

		Assert.IsNotNull(testNullURLHO.getURLException());
		Assert.IsNull(testNullURLHO.getStreamException());
		Assert.IsNull(testNullURLHO.getRequestException());

		//Test bad URL
		const string badURL = "http://bkicia";
		var testBadURLHO = new HouseOutput(badURL);

		testBadURLHO.write(new AlarmSystem(null, null, null));

		Assert.IsNull(testBadURLHO.getURLException());
		Assert.IsNotNull(testBadURLHO.getStreamException());
		Assert.IsNull(testBadURLHO.getRequestException());
	}
}
}
