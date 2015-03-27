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
    class TestExigenceFonctionnel5: BaseTest
    {
        [Test]
        public void Test_StartPage()
        {
            using (WatiN.Core.IE browser = new IE())
            {
                browser.GoTo("http://localhost:53832/");
                browser.WaitForComplete();
                Assert.True(browser.Text.Contains("Taxi Manager"));
            }      
        }
    }
}
