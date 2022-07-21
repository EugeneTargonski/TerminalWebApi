using TerminalWebApi.Exeptions;
using TerminalWebApi.Interfaces;

namespace TerminalWebApi.Models
{
    public class Product: IHasId, IHasCode
    {
        // If one parameter is empty and the second is not, then most likely a human error has occurred.
        private const string emptyDiscountParameterMessage = 
            "Please check the discounted price and discounted quantity. They must both be empty or both have a value.";

        private const string zeroOrNegativePrice =
            "Please check the price. It must be a positive number.";

        private const string zeroOrNegativeQuantity =
            "Please check the quantity. It must be a positive number.";

        private const string emptyCode =
            "Please check the code. It must be not empty.";

        public Product(string code, double price, double? discountPrice = null, int? discountQuantity = null)
        {
            code = code.Trim();
            if (string.IsNullOrEmpty(code))
                throw new TerminalApiException(emptyCode);

            if ( discountPrice != discountQuantity &&
                (discountPrice == null || discountQuantity == null))
                    throw new TerminalApiException(emptyDiscountParameterMessage);

            if (price <= 0 || (discountPrice.HasValue && discountPrice <= 0))  
                throw new TerminalApiException(zeroOrNegativePrice);

            if (discountQuantity.HasValue && discountQuantity <= 0)
                throw new TerminalApiException(zeroOrNegativeQuantity);

            Code = code;
            Price = price;
            DiscountPrice = discountPrice;
            DiscountQuantity = discountQuantity;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public int? DiscountQuantity { get; set; }
    }
}
