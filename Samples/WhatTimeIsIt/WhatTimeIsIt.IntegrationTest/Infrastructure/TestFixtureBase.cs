using System;
using System.Net;
using System.Text.RegularExpressions;
using DeleporterCore.Client;
using NUnit.Framework;

namespace WhatTimeIsIt.IntegrationTest.Infrastructure
{
    public class TestFixtureBase
    {
        protected static string GetPageHtml(string url)
        {
            return new WebClient().DownloadString("http://localhost:8081" + url);
        }

        protected static T ParseSimpleDomElement<T>(string html, string elementId)
        {
            var match = Regex.Match(html, @"<.*? id=['""]" + elementId + @"['""].*?>(.*?)</.*?>", RegexOptions.Singleline);
            string elementText = match.Success ? match.Groups[1].Captures[0].Value : null;
            return (T)Convert.ChangeType(elementText, typeof(T));
        }

        [TearDown]
        public void TearDown()
        {
            // Runs any tidyup tasks in both the local and remote appdomains
            TidyupUtils.PerformTidyup();
            Deleporter.Run(TidyupUtils.PerformTidyup);
        }
    }
}