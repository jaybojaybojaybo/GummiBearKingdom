using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;
using System;
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
            product.ProductId = 1;
            product.Name = "gummi bear";
            product.Description = "yummy treat";
            product.Price = 3;
            product.CategoryId = 2;


            //Assert
            Assert.AreEqual(product.Name, product2.Name);
        }
    }
}
