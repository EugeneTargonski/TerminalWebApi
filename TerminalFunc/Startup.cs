using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using Terminal.Interfaces;
using Terminal.Models;
using TerminalCosmosDB;

[assembly: FunctionsStartup(typeof(TerminalFunc.Startup))]

namespace TerminalFunc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var uri = Environment.GetEnvironmentVariable("KeyVault_CosmosDbUri");
            var key = Environment.GetEnvironmentVariable("KeyVault_CosmosDbKey");
            var dbName = Environment.GetEnvironmentVariable("KeyVault_CosmosDbName");
            var containerName = Environment.GetEnvironmentVariable("KeyVault_CosmosDbContainerName");

            CosmosClient client = new(accountEndpoint: uri, authKeyOrResourceToken: key);
            Database database = client.GetDatabase(id: dbName);
            Container container = database.GetContainer(id: containerName);

            builder.Services.AddScoped<IRepository<Product>>((s) => {
                return new CosmosDbRepository(container);
            });

            builder.Services.AddScoped<ITerminal, Terminal.Terminal>();
        }
    }
}
