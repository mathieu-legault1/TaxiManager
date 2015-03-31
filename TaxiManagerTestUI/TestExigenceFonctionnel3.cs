using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace TaxiManagerTestUI
{
    [TestFixture, RequiresSTA]
    class TestExigenceFonctionnel3 : BaseTest
    {
        [Test]
        public void Test_Projet_STP_TC_1()
        {
            using (IE browser = this.LoginAgency())
            {
                // Va à la page de l'agency
                browser.GoTo("http://localhost:53832/Agency");
                browser.WaitForComplete();

                // Remplie les textfield
                browser.TextField(Find.ByName("Adress")).TypeText("salut");
                browser.TextField(Find.ByName("FirstName")).TypeText("Blos");
                browser.TextField(Find.ByName("LastName")).TypeText("Joe");
                // Click nouveau client
                Assert.False(browser.Button(Find.ByValue("Nouveau client")).Enabled);
            }
        }
    }
}
