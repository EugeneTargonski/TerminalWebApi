namespace Terminal.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(string productCode, double price, double? discountPrice = null, int? discountQuantity = null);
        void Scan(string productCode);
        Task<double> CalculateTotal();
    }
}
