using Terminal.Interfaces;
using Terminal.Models;

namespace TerminalWebApi.API
{
    public static class ProductsApi
    {
        static readonly string _apiPath = "api/products";
        static readonly string _apiPathWithId = "/api/products/{id:int}";

        public static WebApplication ConfigureAPI(WebApplication app)
        {
            app.MapGet(_apiPath, (IRepository<Product> repository) => repository.GetAll());
            app.MapGet(_apiPathWithId, async (int id, IRepository<Product> repository) => await repository.GetAsync(id));
            app.MapDelete(_apiPathWithId, async (int id, IRepository<Product> repository) => await repository.DeleteAsync(id));
            app.MapPost(_apiPath, 
                async (Product product, IRepository<Product> repository) => await repository.CreateAsync(product));
            app.MapPut(_apiPath,
                async (Product product, IRepository<Product> repository) => await repository.UpdateAsync(product));

            return app;
        }
    }
}
