namespace TerminalWebApi.Exeptions
{
    public class TerminalWebApiException: Exception
    {
        public TerminalWebApiException(string? message) : base(message)
        {
        }
    }
}
