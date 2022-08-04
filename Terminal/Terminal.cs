using Terminal.Interfaces;
using Terminal.Models;
using Terminal.Exeptions;

namespace Terminal
{
    public class Terminal : ITerminal
    {
        private readonly IRepository<Product> _repository;
        private readonly Dictionary<string, int> _scannedList;

        public Terminal(IRepository<Product> repository)
        {
            _repository = repository;
            _scannedList = new Dictionary<string, int>();
        }
        public async Task<double> CalculateTotal()
        {
            var sums = _scannedList.Select(p => Sum(p.Key,p.Value));
            var result = await Task.WhenAll(sums);
            return result.Sum();
        }
        public void Scan(string productCode)
        {
            if (!_scannedList.ContainsKey(productCode))
            {
                _scannedList.Add(productCode, 1);
            }
            else
            {
                _scannedList[productCode]++;
            }
        }
        private async Task<double> Sum (string productCode, int productQuantity)
        {
            var product = await _repository.GetAsync(productCode);

            if (product == null)
                throw new TerminalException($"Product with unknown code '{productCode}' was found.");

            if (product.DiscountPrice != null && product.DiscountQuantity != null)
                return productQuantity / (int)product.DiscountQuantity * (double)product.DiscountPrice
                    + productQuantity % (int)product.DiscountQuantity * (double)product.Price;
            else
                return productQuantity * (double)product.Price;
        }
    }
}
