using Moq;
using Terminal;
using Terminal.Exeptions;
using Terminal.Interfaces;
using Terminal.Models;

namespace TerminalTests
{
    public class TerminalTests
    {
        private Mock<IRepository<Product>> FakeRepository { get; set; }
        private readonly IEnumerable<Product> products = new List<Product>() {
            new Product("A", 1.25, 3, 3),
            new Product("B", 4.25),
            new Product("C", 1, 5, 6),
            new Product("D", 0.75)}.AsEnumerable();
        private readonly Terminal.Terminal terminal;


        public TerminalTests()
        {
            FakeRepository = new Mock<IRepository<Product>>();
            var codes = products.Select(p => p.Code).ToList();
            foreach (var code in codes)
                FakeRepository.Setup(r => r.GetAsync(code)).ReturnsAsync(products.FirstOrDefault(p => p.Code == code));
            terminal = new Terminal.Terminal(FakeRepository.Object);
        }

        [Theory]
        [InlineData(13.25, "A", "B", "C", "D", "A", "B", "A")]
        [InlineData(6, "C", "C", "C", "C", "C", "C", "C")]
        [InlineData(7.25, "A", "B", "C", "D")]
        [InlineData(1.25, "A")]
        [InlineData(4.25, "B")]
        [InlineData(1, "C")]
        [InlineData(0.75, "D")]
        public async Task PointOfSaleTerminalCalculate_ReturnsRigthValueAsync(double expectedResult, params string[] data)
        {
            //Arrange 
            foreach(var code in data)
                terminal.Scan(code);

            //Act
            var result = await terminal.CalculateTotal();

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("A", "B", "C", "D", "A", "B", "L")]
        [InlineData("I", "B", "C", "D", "A", "B", "B")]
        [InlineData("C", "C", "C", "E", "C", "C", "C")]
        [InlineData("A", "B", "C", "_")]
        [InlineData("A", "B", "C", " ")]
        [InlineData("A", "B", "C", "")]
        [InlineData("K")]
        public async Task PointOfSaleTerminalCalculate_ShouldThrowErrorWithBadParamAsync(params string[] data)
        {
            //Arrange 
            foreach (var code in data)
                terminal.Scan(code);

            //Act
            Task act() => terminal.CalculateTotal();

            //Assert
            await Assert.ThrowsAsync<TerminalException>(act);
        }
    }
}