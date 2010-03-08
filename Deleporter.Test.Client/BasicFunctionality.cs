using System;
using System.Data.SqlClient;
using System.Web.Hosting;
using DeleporterCore.Client;
using NUnit.Framework;

namespace DeleporterTest.Client
{
    [TestFixture]
    public class BasicFunctionality
    {
        [Test]
        public void CanReceiveValue()
        {
            var serverTime = Deleporter.Run(() => DateTime.UtcNow);
            Assert.AreEqual(DateTime.UtcNow.Year, serverTime.Year, "Retrieved an unlikely current date from the server: {0:yyyy-MM-dd}", serverTime);
        }

        [Test]
        public void CanReceiveValueViaCapturedLocal()
        {
            DateTime serverTime = DateTime.MinValue;
            Deleporter.Run(() => { serverTime = DateTime.UtcNow; });
            Assert.AreEqual(DateTime.UtcNow.Year, serverTime.Year, "Retrieved an unlikely current date from the server: {0:yyyy-MM-dd}", serverTime);
        }

        [Test]
        public void CanPassParameterViaCapturedLocal()
        {
            int a = 5;
            int result = Deleporter.Run(() => {
                int b = 2;
                return a + b;
            });
            Assert.AreEqual(7, result);
        }

        [Test]
        public void CanReturnMultipleValuesViaAnonymousType()
        {
            var result = Deleporter.Run(() => {
                return new {
                    ServerTime = DateTime.UtcNow,
                    WebApplicationPath = HostingEnvironment.ApplicationPhysicalPath
                };
            });
            Assert.AreNotEqual(1, result.ServerTime);
            //StringAssert.Contains("Deleporter.Test.Server", result.AppPath.ToString());
        }
    }
}
