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
            double sum = 0;
            foreach(var productCode in _scannedList)
            {
                var product = await _repository.GetAsync(productCode.Key);

                if (product == null)
                    throw new TerminalException($"Product with unknown code '{productCode}' was found.");

                if (product.DiscountPrice != null && product.DiscountQuantity != null)
                    sum += (int)productCode.Value / (int)product.DiscountQuantity * (double)product.DiscountPrice
                        + (int)productCode.Value % (int)product.DiscountQuantity * (double)product.Price;
                else 
                    sum += (int)productCode.Value * (double)product.Price;
            }
            return sum;
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
    }
}
