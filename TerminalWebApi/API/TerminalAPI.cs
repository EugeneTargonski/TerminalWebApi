using Terminal.Interfaces;
using Terminal.Exeptions;

namespace TerminalWebApi.API
{
    public class TerminalAPI
    {
        static readonly string _apiPath = "api/terminal";

        public static WebApplication ConfigureAPI(WebApplication app)
        {
            app.MapGet(_apiPath, async (IPointOfSaleTerminal terminal, HttpRequest request) =>
            {
                IEnumerable<string>? codes = await request.ReadFromJsonAsync<IEnumerable<string>>();
                if (codes == null)
                    throw new TerminalApiException("Bad Request");

                foreach (var code in codes)
                    terminal.Scan(code);

                return terminal.CalculateTotal();
            });

            return app;
        }
    }
}
