﻿namespace Terminal.Exeptions
{
    public class TerminalApiException : Exception
    {
        public TerminalApiException(string? message) : base(message)
        {
        }
    }
}
