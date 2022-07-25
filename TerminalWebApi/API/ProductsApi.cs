using Terminal.Interfaces;
using Terminal.Models;

namespace TerminalWebApi.API
{
    public static class ProductsApi
    {
        static readonly string _apiPath = "api/products";
        static readonly string _apiPathWithId = "/api/products/{code}";

        public static WebApplication ConfigureAPI(WebApplication app)
        {
            app.MapGet(_apiPath, (IRepository<Product> repository) => repository.GetAll());
            app.MapGet(_apiPathWithId, async (string code, IRepository<Product> repository) => await repository.GetAsync(code));
            app.MapDelete(_apiPathWithId, async (string code, IRepository<Product> repository) => await repository.DeleteAsync(code));
            app.MapPost(_apiPath, 
                async (Product[] products, IRepository<Product> repository) => await CreateMultipleAsync(products, repository));
            app.MapPut(_apiPath,
                async (Product product, IRepository<Product> repository) => await repository.UpdateAsync(product));

            return app;
        }

        private static async Task CreateMultipleAsync(Product[] products, IRepository<Product> repository)
        {
            foreach (Product product in products)
                await repository.CreateAsync(product);
        }
    }
}
