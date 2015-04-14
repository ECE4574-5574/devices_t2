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
}

}
