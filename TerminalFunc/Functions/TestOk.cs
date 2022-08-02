using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TerminalFunc.Functions
{
    public static class TestOk
    {
        [FunctionName("KeyVaultTest")]
        public static IActionResult KeyVaultTest(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test")] HttpRequest req, ILogger log)
        {
            return new OkObjectResult("Ok");
        }
    }
}
