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
    class TestExigenceFonctionnel6: BaseTest
    {
        [Test]
        public void Test_Projet_STP_TC_5()
        {
            using (IE browser = this.LoginAgency())
            {
                // Va à la page de l'agency
                browser.GoTo("http://localhost:53832/Agency");
                browser.WaitForComplete();

                // Remplie les textfield
                browser.TextField(Find.ByName("Adress")).TypeText("1100 rue Notre-Dame Ouest, Montréal");
                browser.TextField(Find.ByName("FirstName")).TypeText("Blos");
                browser.TextField(Find.ByName("LastName")).TypeText("Joe");

                // Catch l'adresse dans le systeme
                var searchAdress = browser.TextField(Find.ByName("Adress")).Text;
                Assert.True(searchAdress.Equals("1120 Rue Notre Dame Ouest, Montréal, QC H3C 0J8, Canada"));

                // Click nouveau client
                browser.Button(Find.ByValue("Nouveau client")).Click();
                browser.WaitForComplete();

                // Les champs sont supposé être effacé.
                Assert.True(browser.TextField(Find.ByName("FirstName")).Text == null);
                Assert.True(browser.TextField(Find.ByName("LastName")).Text == null);
                Assert.True(browser.TextField(Find.ByName("Adress")).Text == null);
            }
        }
    }
}
