using Terminal.Interfaces;
using TerminalWebApi.Exeptions;

namespace TerminalWebApi.API
{
    public class TerminalAPI
    {
        const string section = "TerminalApi";
        const string keyApiPath = "ApiPath";

        public static WebApplication ConfigureAPI(WebApplication app)
        {
            string apiPath = app.Configuration.GetSection(section).GetValue<string>(keyApiPath);

            app.MapGet(apiPath, async (ITerminal terminal, HttpRequest request) =>
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
