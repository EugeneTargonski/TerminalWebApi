using Terminal.Interfaces;
using Terminal.Models;

namespace TerminalWebApi.API
{
    public static class ProductsApi
    {
        const string section = "ProductsApi";
        const string keyApiPath = "ApiPath";
        const string keyApiPathParameterized = "ApiPathParameterized";

        public static WebApplication ConfigureAPI(WebApplication app)
        {
            string apiPath = app.Configuration.GetSection(section).GetValue<string>(keyApiPath);
            string apiPathWithId = app.Configuration.GetSection(section).GetValue<string>(keyApiPathParameterized);

            app.MapGet(apiPath, (IRepository<Product> repository) => repository.GetAll());
            app.MapGet(apiPathWithId, async (string code, IRepository<Product> repository) => await repository.GetAsync(code));
            app.MapDelete(apiPathWithId, async (string code, IRepository<Product> repository) => await repository.DeleteAsync(code));
            app.MapPost(apiPath, 
                async (Product[] products, IRepository<Product> repository) => await CreateMultipleAsync(products, repository));
            app.MapPut(apiPath,
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
