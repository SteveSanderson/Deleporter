using System;
using System.IO;
using System.Reflection;
using System.Net;
using NUnit.Framework;

namespace DeleporterTest.Client
{
    [SetUpFixture]
    public class Setup
    {
        private const string RelativePathToServerApp = "..\\..\\..\\Deleporter.Test.Server";
        private const string ServerAppUrl = "http://localhost:8081/";
        private const string ServerAppExpectedHtml = "Hello";

        [SetUp]
        public void RestartTestApp()
        {
            RecycleServerAppDomain();
            RestartServer();
        }

        private static void RestartServer()
        {
            using(var wc = new WebClient())
            {
                var html = wc.DownloadString(ServerAppUrl);
                if (!html.Contains(ServerAppExpectedHtml))
                    Assert.Fail("Can't continue with tests: the test app isn't running at " + ServerAppUrl);
            }
        }

        private static void RecycleServerAppDomain()
        {
            var currentAssemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;
            var currentAssemblyDirectory = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(currentAssemblyCodeBase).Path));

            string testServerRootDirectory = Path.GetFullPath(Path.Combine(currentAssemblyDirectory, RelativePathToServerApp));
            string webConfigFileName = Path.Combine(testServerRootDirectory, "Web.config");

            File.SetLastWriteTime(webConfigFileName, DateTime.Now);
        }
    }
}