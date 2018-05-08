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
using System.Threading.Tasks;

namespace GummiBearKingdomTests.Models
{
    [TestClass]
    public class IntegrationTests : IDisposable
    {
        EFProductRepository db = new EFProductRepository(new GummiDbContext());

        public virtual void Dispose()
        {
            db.RemoveAll();
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product(1, "plummi beer", "beer made of plums", 3, 2);

            //Act
            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            //Assert
            Assert.AreEqual(collection.Count, 1);
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void DB_GetViewResultIndex_ViewResult()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DB_PostProductAfterEdit_Product()
        {
            //Arrange 
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product(1, "plummi beer", "beer made of plums", 3, 2);
            controller.Create(testProduct);

            //Act
            testProduct.Price = 4;
            var result = controller.Edit(testProduct.ProductId, testProduct);
            var indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            //Assert
            Assert.AreEqual(4, collection[0].Price);
        }

        [TestMethod]
        public void DB_PostDeleteProduct_Product()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct1 = new Product(4, "plummi beer", "beer made of plums", 3, 2);
            testProduct1.Reviews = new List<Review>();
            controller.Create(testProduct1);

            Product testProduct2 = new Product(5, "gummi bear", "yummy treat", 3, 2);
            controller.Create(testProduct2);

            //Act
            var result = controller.DeleteConfirmed(testProduct1.ProductId) as RedirectToActionResult;

            var indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            //Assert
            Assert.AreEqual(5, collection[0].ProductId);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DB_PostDeleteAllProducts_Product()
        {
            //Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct1 = new Product(1, "plummi beer", "beer made of plums", 3, 2);
            controller.Create(testProduct1);

            Product testProduct2 = new Product(2, "gummi bear", "yummy treat", 3, 2);
            controller.Create(testProduct2);

            //Act
            var productView = (controller.Index() as ViewResult).ViewData.Model as List<Product>;
            RedirectToActionResult result = controller.DeleteAllConfirmed() as RedirectToActionResult;
            var indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(0, collection.Count);
            Assert.AreEqual(2, productView.Count);
        }

        [TestMethod]
        public void DB_PostIndexReviews_Review()
        {
            //Arrange
            ReviewsController controller = new ReviewsController(db);
            Review review1 = new Review();
            review1.ReviewId = 1;
            review1.Author = "A";
            review1.Content_Body = "awesome";
            review1.rating = 1;
            review1.ProductId = 1;

            //Act
            var result = (RedirectToActionResult)controller.Create(review1);
            List<Review> collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

            //Assert
            CollectionAssert.Contains(collection, review1);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
