namespace Terminal.Interfaces
{
    public interface ITerminal
    {
        void Scan(string productCode);
        Task<double> CalculateTotal();
    }
}
