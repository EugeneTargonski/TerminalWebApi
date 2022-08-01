using Newtonsoft.Json;
using Terminal.Models;

namespace TerminalCosmosDB
{
    internal record class CosmosProduct : Product
    {
        public CosmosProduct(string code, double price, double? discountPrice = null, int? discountQuantity = null) : base(code, price, discountPrice, discountQuantity)
        {
            Id = code;
        }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
