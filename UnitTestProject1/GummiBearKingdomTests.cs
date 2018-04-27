using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;

namespace GummiBearKingdom.Tests
{
    [TestClass]
    public class GummiBearKingdomTests
    {
        [TestMethod]
        public void GetName_ReturnsProductName_String()
        {
            //Arrange
            var product = new Product();
            product.Name = "toy";

            //Act
            var result = product.Name;

            //Assert
            Assert.AreEqual("toy", result);
        }
    }
}
