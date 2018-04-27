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

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            //Arrange
            DbSetup();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsItems_Collection()
        {
            //Arrange
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            Product testProduct = new Product();
            testProduct.ProductId = 1;
            testProduct.Name = "gummi bear";
            testProduct.Description = "yummy treat";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;

            //Act
            ViewResult indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            //Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            //Arrange
            Product testProduct = new Product();
            testProduct.ProductId = 1;
            testProduct.Name = "gummi bear";
            testProduct.Description = "yummy treat";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var resultView = controller.Create(testProduct) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            //Arrange
            Product testProduct = new Product();
            testProduct.ProductId = 1;
            testProduct.Name = "gummi bear";
            testProduct.Description = "yummy treat";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var resultView = controller.Details(testProduct.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Product));
        }

        [TestMethod]
        public void Mock_PostResultViewEdit_ViewResult()
        {
            //Arrange
            Product testProduct = new Product();
            testProduct.ProductId = 2;
            testProduct.Name = "plummi beer";
            testProduct.Description = "beer made of plums";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var resultView = controller.Edit(testProduct.ProductId) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
        }

        [TestMethod]
        public void Mock_PostResultViewDelete_ViewResult()
        {
            //Arrange
            Product testProduct = new Product();
            testProduct.ProductId = 2;
            testProduct.Name = "plummi beer";
            testProduct.Description = "beer made of plums";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var resultView = controller.Delete(testProduct.ProductId) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
        }

    }
}
