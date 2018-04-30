﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GummiBearKingdom.Models;
using GummiBearKingdom.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GummiBearKingdomTests.ControllerTests
{
    [TestClass]
    public class ProductsControllerTest : IDisposable
    {
        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddEntityFrameworkMySql()
                .AddDbContext<GummiDbContext>(options =>
                options
                .UseMySql(Configuration["ConnectionStrings:TestConnection"]));
        }

        public virtual void Dispose()
        {
            GummiTestDbContext context = new GummiTestDbContext();
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE products");
        }

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
            var resultView = (ViewResult)controller.Create();

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


        //BEGINNING OF INTEGRATION TESTS
        EFProductRepository db = new EFProductRepository(new GummiTestDbContext());

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product();
            testProduct.ProductId = 2;
            testProduct.Name = "plummi beer";
            testProduct.Description = "beer made of plums";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;
            testProduct.Reviews = new List<Review>();

            //Act
            controller.Create(testProduct);
            List<Product> collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            //Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void DB_GetViewResultIndex_ActionResult()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void DB_PostProductAfterEdit_Product()
        {
            //Arrange 
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product();
            testProduct.ProductId = 2;
            testProduct.Name = "plummi beer";
            testProduct.Description = "beer made of plums";
            testProduct.Price = 3;
            testProduct.CategoryId = 2;
            testProduct.Reviews = new List<Review>();
            controller.Create(testProduct);

            //Act
            testProduct.ProductId = 2;
            testProduct.Name = "plummi beer";
            testProduct.Description = "beer made of plums";
            testProduct.Price = 4;
            testProduct.CategoryId = 2;
            testProduct.Reviews = new List<Review>();
            var result = (ViewResult)controller.Edit(testProduct.ProductId);
            List<Product> collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            //Assert
            Assert.AreEqual(4, collection[0].Price);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DB_PostDeleteProduct_Product()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct1 = new Product();
            testProduct1.ProductId = 1;
            testProduct1.Name = "plummi beer";
            testProduct1.Description = "beer made of plums";
            testProduct1.Price = 3;
            testProduct1.CategoryId = 2;
            testProduct1.Reviews = new List<Review>();
            controller.Create(testProduct1);

            Product testProduct2 = new Product();
            testProduct2.ProductId = 2;
            testProduct2.Name = "gummi bear";
            testProduct2.Description = "yummy treat";
            testProduct2.Price = 3;
            testProduct2.CategoryId = 2;
            controller.Create(testProduct2);

            //Act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(testProduct1.ProductId);
            List<Product> collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            //Assert
            Assert.AreEqual(2, collection[0].ProductId);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DB_PostDeleteAllProducts_Product()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct1 = new Product();
            testProduct1.ProductId = 1;
            testProduct1.Name = "plummi beer";
            testProduct1.Description = "beer made of plums";
            testProduct1.Price = 3;
            testProduct1.CategoryId = 2;
            testProduct1.Reviews = new List<Review>();
            controller.Create(testProduct1);

            Product testProduct2 = new Product();
            testProduct2.ProductId = 2;
            testProduct2.Name = "gummi bear";
            testProduct2.Description = "yummy treat";
            testProduct2.Price = 3;
            testProduct2.CategoryId = 2;
            controller.Create(testProduct2);

            //Act
            GummiTestDbContext context = new GummiTestDbContext();
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE products");
            var result = (RedirectToActionResult)controller.DeleteAll();
            List<Product> collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            //Assert
            Assert.AreEqual(0, collection.Count);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
