using System;
using System.Collections.Generic;
using TaxiManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiManager.Controllers;
using System.Web.Mvc;

namespace TaxiManagerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Taxi Manager", result.ViewBag.Title);
        }
    }
}
