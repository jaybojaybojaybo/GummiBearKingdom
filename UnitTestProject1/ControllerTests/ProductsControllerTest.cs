using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GummiBearKingdom.Models;
using GummiBearKingdom.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GummiBearKingdomTests.ControllerTests
{
    [TestClass]
    public class ProductsControllerTest
    {
        Mock<IGummiRepository> mock = new Mock<IGummiRepository>();

        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{
                    ProductId = 1,
                    Name = "gummi bear",
                    Description = "yummy treat",
                    Price = 3,
                    CategoryId = 2 },
                new Product{
                    ProductId = 2,
                    Name = "dummy bear",
                    Description = "funny toy",
                    Price = 12,
                    CategoryId = 1 }
            }.AsQueryable());
        }

        [TestMethod]
        public void ProductsController_IndexModelContainsCorrectData_List()
        {
            //Arrange
            ProductsController controller = new ProductsController();
            IActionResult actionResult = controller.Index();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;

            //Act 
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult()
        {
            //Arrange
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
    }
}
