using Terminal.Interfaces;
using TerminalWebApi.Exeptions;

namespace TerminalWebApi.API
{
    public class TerminalAPI
    {
        static readonly string _apiPath = "api/terminal";

        public static WebApplication ConfigureAPI(WebApplication app)
        {
            app.MapGet(_apiPath, async (ITerminal terminal, HttpRequest request) =>
            {
                IEnumerable<string>? codes = await request.ReadFromJsonAsync<IEnumerable<string>>();
                if (codes == null)
                    throw new TerminalWebApiException("Bad Request");

                foreach (var code in codes)
                    terminal.Scan(code);

                return terminal.CalculateTotal();
            });

            return app;
        }
    }
}
