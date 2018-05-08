using System;
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
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE reviews");
        }

        Mock<IGummiRepository> mock = new Mock<IGummiRepository>();

        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product(1, "gummi bear", "yummy treat", 3, 2),
                new Product(1, "dummy bear", "funny toy", 12, 1)
            }.AsQueryable());
        }

        [TestMethod]
        public void ProductsController_IndexModelContainsCorrectData_List()
        {
            //Arrange
            ProductsController controller = new ProductsController();
            IActionResult actionResult = (IActionResult)controller.Index();
            var indexView = new ProductsController(mock.Object);

            //Act 
            var asyncTask = indexView.Index() as ViewResult;
            var result = asyncTask.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ViewResult()
        {
            //Arrange
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            //Arrange
            DbSetup();
            var indexView = new ProductsController(mock.Object);


            //Act
            var asyncTask = indexView.Index() as ViewResult;
            var result = asyncTask.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsItems_Collection()
        {
            //Arrange
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            Product testProduct = new Product(1, "gummi bear", "yummy treat", 3, 2);

            //Act
            var indexView = new ProductsController(mock.Object);
            var asyncTask = indexView.Index() as ViewResult;
            List<Product> collection = asyncTask.ViewData.Model as List<Product>;

            //Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            //Arrange
            Product testProduct = new Product(1, "gummi bear", "yummy treat", 3, 2);

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
            DbSetup();
            Product testProduct = new Product(9, "gummi bear", "yummy treat", 3, 2);
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            ViewResult resultView = controller.Details(testProduct.ProductId) as ViewResult;
            Product model = resultView.ViewData.Model as Product;

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_PostResultViewEdit_ViewResult()
        {
            //Arrange
            Product testProduct = new Product(2, "plummi beer", "beer made of plums", 3, 2);

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
            Product testProduct = new Product(2, "plummi beer", "beer made of plums", 3, 2);

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            //Act
            var resultView = controller.Delete(testProduct.ProductId) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
        }
    }
}
