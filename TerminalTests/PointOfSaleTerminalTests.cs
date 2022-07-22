using Moq;
using Terminal.Interfaces;
using Terminal.Models;
using Terminal;

namespace TerminalTests
{
    public class PointOfSaleTerminalTests
    {
        private Mock<IRepository<Product>> FakeRepository { get; set; }
        private readonly IEnumerable<Product> products = new List<Product>() {
            new Product("A", 1.25, 3, 3) { Id = 1 },
            new Product("B", 4.25) { Id = 2 },
            new Product("C", 1, 5, 6) { Id = 3 },
            new Product("D", 0.75) { Id = 4 }}.AsEnumerable();
        private readonly PointOfSaleTerminal terminal;


        public PointOfSaleTerminalTests()
        {
            FakeRepository = new Mock<IRepository<Product>>();
            var codes = products.Select(p => p.Code).ToList();
            foreach (var code in codes)
                FakeRepository.Setup(r => r.GetByCodeAsync(code)).ReturnsAsync(products.FirstOrDefault(p => p.Code == code));
            terminal = new PointOfSaleTerminal(FakeRepository.Object);
        }

        [Theory]
        [InlineData(13.25, "A", "B", "C", "D", "A", "B", "A")]
        [InlineData(6, "C", "C", "C", "C", "C", "C", "C")]
        [InlineData(7.25, "A", "B", "C", "D")]
        public async void PointOfSaleTerminalCalculate_ReturnsRigthValueAsync(double expectedResult, params string[] data)
        {
            //Arrange 
            foreach(var code in data)
                terminal.Scan(code);

            //Act
            var result = await terminal.CalculateTotal();

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}