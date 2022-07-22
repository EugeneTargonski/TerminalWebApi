using Terminal.Exeptions;
using Terminal.Interfaces;

namespace Terminal.Models
{
    public class Product: IHasId, IHasCode
    {
        // If one parameter is empty and the second is not, then most likely a human error has occurred.
        private const string emptyDiscountParameterMessage = 
            "Please check the discounted price and discounted quantity. They must both be empty or both have a value.";

        private const string zeroOrNegativePriceMessage =
            "Please check the price. It must be a positive number.";

        private const string zeroOrNegativeQuantityMessage =
            "Please check the quantity. It must be a positive number.";

        private const string emptyCodeMessage =
            "Please check the code. It must be not empty.";

        public Product(string code, double price, double? discountPrice = null, int? discountQuantity = null)
        {
            code = code.Trim();
            if (string.IsNullOrEmpty(code))
                throw new TerminalException(emptyCodeMessage);
            Code = code;
            SetPricing(price, discountPrice, discountQuantity);
        }

        public int Id { get; set;  }
        public string Code { get; }
        public double Price { get; private set; }
        public double? DiscountPrice { get; private set; }
        public int? DiscountQuantity { get; private set; }

        public void SetPricing(double price, double? discountPrice = null, int? discountQuantity = null)
        {
            if (discountPrice != discountQuantity &&
                (discountPrice == null || discountQuantity == null))
                throw new TerminalException(emptyDiscountParameterMessage);

            if (price <= 0 || (discountPrice.HasValue && discountPrice <= 0))
                throw new TerminalException(zeroOrNegativePriceMessage);

            if (discountQuantity.HasValue && discountQuantity <= 0)
                throw new TerminalException(zeroOrNegativeQuantityMessage);

            Price = price;
            DiscountPrice = discountPrice;
            DiscountQuantity = discountQuantity;
        }
    }
}
