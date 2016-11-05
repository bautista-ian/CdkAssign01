using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CdkAssign01;
using CdkAssign01.Controllers;
using CdkAssign01.BAL;

namespace CdkAssign01.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index(ICustomersRepository customersRepo, IOrdersRepository ordersRepo)
        {
            // Arrange
            HomeController controller = new HomeController(customersRepo, ordersRepo);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
