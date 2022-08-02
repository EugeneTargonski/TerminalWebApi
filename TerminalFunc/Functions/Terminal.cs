using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Terminal.Models;
using Terminal.Interfaces;
using System.Collections.Generic;

namespace TerminalFunc.Functions
{
    public class Terminal
    {
         private readonly ITerminal _terminal;

        public Terminal(IRepository<Product> repository, ITerminal terminal)
        {
            _repository = repository;
            _terminal = terminal;
        }

        [FunctionName("TerminalGet")]
        public async Task<IActionResult> TerminalGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "terminal")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            IEnumerable<string> codes = JsonConvert.DeserializeObject<IEnumerable<string>>(requestBody);
            foreach (string code in codes)
                _terminal.Scan(code);
            var result = await _terminal.CalculateTotal();
            return new OkObjectResult(result);
        }
    }
}
