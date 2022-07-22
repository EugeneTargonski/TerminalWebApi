using Moq;
using Terminal;
using Terminal.Exeptions;
using Terminal.Interfaces;
using Terminal.Models;

namespace TerminalTests
{
    public class ProductTests
    {
        [Theory]
        [InlineData("AA", 13.25, null, 1)]
        [InlineData("AA", 13.25, 1.25, null)]
        public void ConstructorDiscountParameters_BothShouldBeOrEmptyOrNonEmpty(
            string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange

            //Act
            void act() => new Product(code, price, discountPrice, discountQuantity);

            //Assert
            Assert.Throws<TerminalException>(act);
        }

        [Theory]
        [InlineData("AA", 13.25, null, 1)]
        [InlineData("AA", 13.25, 1.25, null)]
        public void SetPricingDiscountParameters_BothShouldBeOrEmptyOrNonEmpty(
            string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange
            var product = new Product(code, 1);

            //Act
            void act() => product.SetPricing(price, discountPrice, discountQuantity);

            //Assert
            Assert.Throws<TerminalException>(act);
        }

        [Theory]
        [InlineData("", 1.25, null, null)]
        [InlineData(" ", 1.25, 3.5, 3)]
        [InlineData("   ", 1.25, null, null)]
        public void ProductCode_ShouldNotBeEmpty(
            string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange

            //Act
            void act() => new Product(code, price, discountPrice, discountQuantity);

            //Assert
            Assert.Throws<TerminalException>(act);
        }

        [Theory]
        [InlineData("A", 1.25, null, null)]
        [InlineData("AS", 1.25, 4.2, 5)]
        [InlineData("AZA ", 2.8, null, null)]
        [InlineData(" FG", 1.25, 3.5, 3)]
        public void Constructor_ShouldCreateRightInstances(
            string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange

            //Act
            var product = new Product(code, price, discountPrice, discountQuantity);

            //Assert
            Assert.Equal(code.Trim(), product.Code);
            Assert.Equal(price, product.Price);
            Assert.Equal(discountPrice, product.DiscountPrice);
            Assert.Equal(discountQuantity, product.DiscountQuantity);
        }

        [Theory]
        [InlineData("A", 1.25, null, null)]
        [InlineData("AS", 0.95, 4.2, 5)]
        [InlineData("AZA ", 2.8, null, null)]
        [InlineData(" FG", 1.25, 3.5, 3)]
        public void SetPricing_ShouldChageValues(
        string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange
            var product = new Product(code, 1);

            //Act
            product.SetPricing(price, discountPrice, discountQuantity);

            //Assert
            //Assert
            Assert.Equal(price, product.Price);
            Assert.Equal(discountPrice, product.DiscountPrice);
            Assert.Equal(discountQuantity, product.DiscountQuantity);
        }

        [Theory]
        [InlineData("A", -1.25, null, null)]
        [InlineData("AS", 0.95, -4.2, 5)]
        [InlineData("AZA ", 0, null, null)]
        [InlineData(" FG", 1.25, 3.5, -3)]
        [InlineData("AFG", 1.25, 0, 3)]
        [InlineData("AFG2", 1.25, 3.5, 0)]
        public void Constructor_ShouldThrowWithNegativeOrZeroPriceOrQuantity(
            string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange

            //Act
            void act() => new Product(code, price, discountPrice, discountQuantity);

            //Assert
            Assert.Throws<TerminalException>(act);
        }

        [Theory]
        [InlineData("A", -1.25, null, null)]
        [InlineData("AS", 0.95, -4.2, 5)]
        [InlineData("AZA ", 0, null, null)]
        [InlineData(" FG", 1.25, 3.5, -3)]
        [InlineData("AFG", 1.25, 0, 3)]
        [InlineData("AFG2", 1.25, 3.5, 0)]
        public void SetPricing_ShouldThrowWithNegativeOrZeroPriceOrQuantity(
            string code, double price, double? discountPrice, int? discountQuantity)
        {
            //Arrange
            var product = new Product(code, 1);

            //Act
            void act() => product.SetPricing(price, discountPrice, discountQuantity);

            //Assert
            //Assert
            Assert.Throws<TerminalException>(act);
        }
    }
}
