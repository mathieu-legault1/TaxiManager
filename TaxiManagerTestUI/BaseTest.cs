using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace TaxiManagerTestUI
{
    [RequiresSTA]
    class BaseTest
    {

        protected IE LoginAgency()
        {
            IE browser = new IE();

            browser.GoTo("http://localhost:53832/Account/Login");
            browser.WaitForComplete();

            browser.TextField(Find.ByName("UserName")).TypeText("Agency");
            browser.TextField(Find.ByName("Password")).TypeText("Agency123[");

            browser.Button(Find.ByValue("Connexion")).Click();
            browser.WaitForComplete();

            return browser;
        }

        protected IE LoginTaxi()
        {
            IE browser = new IE();

            browser.GoTo("http://localhost:53832/Account/Login");
            browser.WaitForComplete();

            browser.TextField(Find.ByName("UserName")).TypeText("Taxi");
            browser.TextField(Find.ByName("Password")).TypeText("Taxi123[");

            browser.Button(Find.ByValue("Connexion")).Click();
            browser.WaitForComplete();

            return browser;
        }
    }
}
