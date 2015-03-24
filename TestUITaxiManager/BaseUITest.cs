using System.Configuration;
using NUnit.Framework;
using WatiN.Core;

namespace TestUITaxiManager
{
    public class BaseUITest
    {
        public IE browser;
        public string rootURL;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            rootURL = ConfigurationManager.AppSettings["RootURL"];
            // hide IE browser during tests
            Settings.Instance.MakeNewIeInstanceVisible = false;
            browser = new IE(rootURL + "Account/LogOn");
            browser.WaitForComplete();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if (browser != null)
            {
                browser.ForceClose();
            }
        }
    }
}