using NUnit.Framework;
using System;
using api;
using Newtonsoft.Json;

namespace api_tests
{
    [TestFixture]
    public class APITest
    {
    [SetUp]
    public void Init()
    {
    }

	[Test ()]
    public void TestServerInput()
    {
        var input = new ServerInput();
		var device = new LightSwitch(input, null);
        var response = input.read(device);
        Assert.AreEqual(200, response);
    }

	[Test ()]
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

	[Test ()]
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

	[Test ()]
	public void TestDeviceDeserialization()
	{
		string invalid_string = "";

		var invalid_object = Interfaces.DeserializeDevice(invalid_string, null, null);
		Assert.AreEqual(invalid_object, null);

		string valid_string = "{class: \"LightSwitch\", enabled: true, Brightness: 1.0}";
		var valid_switch = Interfaces.DeserializeDevice(valid_string, null, null);

		Assert.IsInstanceOf<LightSwitch>(valid_switch);
		var ls = valid_switch as LightSwitch;
		Assert.IsTrue(ls.Enabled);
		Assert.AreEqual(ls.Value.Brightness, 1.0);
	}
}

}
