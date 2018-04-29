using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GummiBearKingdom.Tests
{
    [TestClass]
    public class GummiBearKingdomTests : IDisposable
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

        [TestMethod]
        public void Constructor_ReturnsProductName_String()
        {
            //Arrange
            //Comment in below, when testing constructor
            //var product = new Product(1, "gummi bear", "yummy treat", 3, 2);
            //Comment in below, when testing controllers
            var product = new Product();
            product.ProductId = 1;
            product.Name = "gummi bear";
            product.Description = "yummy treat";
            product.Price = 3;
            product.CategoryId = 2;


            //Act
            var result = product;

            //Assert
            Assert.AreEqual("gummi bear", result.Name);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfProductsAreTheSame_Product()
        {
            //Arrange
            //Comment in below, when testing constructor
            //var product = new Product(1, "gummi bear", "yummy treat", 3, 2);
            //Comment in below, when testing controllers
            var product = new Product();
            product.ProductId = 1;
            product.Name = "gummi bear";
            product.Description = "yummy treat";
            product.Price = 3;
            product.CategoryId = 2;

            //Comment in below, when testing constructor
            //var product = new Product(1, "gummi bear", "yummy treat", 3, 2);
            //Comment in below, when testing controllers
            var product2 = new Product();
            product2.ProductId = 1;
            product2.Name = "gummi bear";
            product2.Description = "yummy treat";
            product2.Price = 3;
            product2.CategoryId = 2;


            //Act
            var result = product;
            var result2 = product2;

            //Assert
            Assert.AreEqual(result.ProductId, result2.ProductId);
        }

        [TestMethod]
        public void GetAvgRating_ReturnsAverageRatings_Int()
        {
            //Arrange
            Product product = new Product();
            product.ProductId = 1;
            product.Name = "gummi bear";
            product.Description = "yummy treat";
            product.Price = 3;
            product.CategoryId = 2;
            product.Reviews = new List<Review>();

            Review review1 = new Review();
            review1.ReviewId = 1;
            review1.Author = "A";
            review1.Content_Body = "awesome";
            review1.rating = 1;
            review1.ProductId = 1;

            Review review2 = new Review();
            review2.ReviewId = 1;
            review2.Author = "Z";
            review2.Content_Body = "bad";
            review2.rating = 5;
            review2.ProductId = 1;

            product.Reviews.Add(review1);
            product.Reviews.Add(review2);

            //Act
            int avgRate = product.GetAvgRate(product);

            //Assert
            Assert.AreEqual(3, avgRate);
        }
    }
}
