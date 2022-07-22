namespace Terminal.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        Task SetPricing(string productCode, double price, double? discountPrice = null, int? discountQuantity = null);
        void Scan(string productCode);
        Task<double> CalculateTotal();
    }
}
