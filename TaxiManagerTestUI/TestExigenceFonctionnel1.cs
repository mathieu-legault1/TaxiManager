using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatiN.Core;

namespace TaxiManagerTestUI
{
    [TestFixture, RequiresSTA]
    class TestExigenceFonctionnel1: BaseTest
    {
        [Test]
        public void Test_Projet_STP_TC_4()
        {
            using(IE browser = this.LoginTaxi())
                Assert.True(browser.ContainsText("Taxi."));
        }

        [Test]
        public void Projet_STP_TC_6()
        {
            using (IE browser = this.LoginAgency())
            {
                Assert.True(browser.ContainsText("Agence."));
            }
        }
    }
}
