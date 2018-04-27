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
            var product = new Product(1, "gummi bear", "yummy treat", 3, 2);
            //product.ProductId = 1;
            //product.Name = "gummi bear";
            //product.Description = "yummy treat";
            //product.Price = 3;
            //product.CategoryId = 2;


            //Act
            var result = product;

            //Assert
            Assert.AreEqual("gummi bear", result.Name);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfProductsAreTheSame_Product()
        {
            //Arrange
            var product = new Product(1, "gummi bear", "yummy treat", 3, 2);
            //product.ProductId = 1;
            //product.Name = "gummi bear";
            //product.Description = "yummy treat";
            //product.Price = 3;
            //product.CategoryId = 2;

            var product2 = new Product(1, "gummi bear", "yummy treat", 3, 2);
            //product2.ProductId = 1;
            //product2.Name = "gummi bear";
            //product2.Description = "yummy treat";
            //product2.Price = 3;
            //product2.CategoryId = 2;


            //Act
            var result = product;
            var result2 = product2;

            //Assert
            Assert.AreEqual(result, result2);
        }
    }
}
