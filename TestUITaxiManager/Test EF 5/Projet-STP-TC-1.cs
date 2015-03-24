using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUITaxiManager.Test_EF_5
{
    public class Projet_STP_TC_1 : BaseUITest
    {

        [Test]
        public void TestAddClient()
        {

        }

        public void LogginAgency()
        {
            browser.GoTo(rootURL + "Account/LogOn");
            browser.WaitForComplete();
            // find the username and password textboxes by ID
            //browser.TextField(Find.ById("username")).TypeText("robert.corvus");
            //browser.TextField(Find.ById("password")).TypeText("secretpw");

            // always use ClickNoWait and WaitForComplete to prevent issues with IE8 browser (already being open, etc.)

            // find the Log On submit button by value because it doesn't have an ID
            //browser.Button(Find.ByValue("Log On")).ClickNoWait();
            //browser.WaitForComplete();
            //Assert.AreEqual(rootURL, browser.Url);
            //Assert.IsTrue(browser.Text.Contains("Welcome robert.corvus!"));
        }
    }
}