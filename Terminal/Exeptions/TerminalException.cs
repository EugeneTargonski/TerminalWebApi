namespace Terminal.Exeptions
{
    internal class TerminalException : Exception
    {
        public TerminalException(string? message) : base(message)
        {
        }
    }
}
