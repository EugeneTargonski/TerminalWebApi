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
using System.Linq;

namespace TerminalFunc.Functions
{
    public class Products
    {
        private readonly IRepository<Product> _repository;

        public Products(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [FunctionName("ProductsGetById")]
        public async Task<IActionResult> ProductsGetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{code}")] HttpRequest req, ILogger log, string code)
        {
            var result = await _repository.GetAsync(code);
            return new OkObjectResult(result);
        }

        [FunctionName("ProductsDeleteById")]
        public async Task<IActionResult> ProductsDeleteById(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "products/{code}")] HttpRequest req, ILogger log, string code)
        {
            await _repository.DeleteAsync(code);
            return new OkObjectResult("Deleted");
        }

        [FunctionName("ProductsGet")]
        public IActionResult ProductsGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequest req, ILogger log)
        {
            var result = _repository.GetAll();
            return new OkObjectResult(result);
        }

        [FunctionName("ProductsPost")]
        public async Task<IActionResult> ProductsPost(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req, ILogger log)
        {
            int i = 0;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product[] products = JsonConvert.DeserializeObject<Product[]>(requestBody);
            
            var tasks = products.Select(p => _repository.CreateAsync(p));
            await Task.WhenAll(tasks);

            return new OkObjectResult($"{i} product(s) created/updated");
        }
    }
}
