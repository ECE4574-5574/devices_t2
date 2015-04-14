using NUnit.Framework;
using System;

namespace api_tests
{
    [TestFixture]
    public class APITest
    {
        [SetUp]
        public void Init()
        {
            connection = new Connection(Uri server);

            IDeviceInput inp;
            IDeviceOutput outp;
            Dev testDev = new Dev(inp, outp);
        }

        [Test]
        public void TestServerInput()
        {
            input = new ServerInput(connection);
            response = input.write(testDev);
            Assert.AreEqual(200, response);
        }
    }
}

