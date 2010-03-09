using System;
using DeleporterCore.Client;
using Moq;
using NUnit.Framework;
using WhatTimeIsIt.IntegrationTest.Infrastructure;
using WhatTimeIsIt.Services;

namespace WhatTimeIsIt.IntegrationTest
{
    /// <summary>
    /// Of course, in a real application, you'd be using a browser automation tool such as WatiN or Selenium to automate the UI.
    /// In this example, I'm directly issuing HTTP requests and parsing the results just so you can run it without needing to set up any browser automation tool.
    /// </summary>
    [TestFixture]
    public class SimpleTest : TestFixtureBase
    {
        [Test]
        public void DisplaysCurrentYear()
        {
            var html = GetPageHtml("/");
            var displayedDate = ParseSimpleDomElement<DateTime>(html, "date");
            Assert.AreEqual(DateTime.Now.Year, displayedDate.Year);
        }

        [Test]
        public void DisplaysSpecialMessageIfWebServerHasSomehowGoneBackInTime()
        {
            // Inject a mock IDateProvider, setting the clock back to 1975
            var dateToSimulate = new DateTime(1975, 1, 1);
            Deleporter.Run(() => {
                var mockDateProvider = new Mock<IDateProvider>();
                mockDateProvider.Setup(x => x.CurrentDate).Returns(dateToSimulate);
                NinjectControllerFactoryUtils.TemporarilyReplaceBinding(mockDateProvider.Object);
            });

            // Now see what it displays
            var html = GetPageHtml("/");
            Assert.AreEqual(1975, ParseSimpleDomElement<DateTime>(html, "date").Year);
            StringAssert.Contains("The world wide web hasn't been invented yet", ParseSimpleDomElement<string>(html, "extraInfo"));
        }
    }
}
