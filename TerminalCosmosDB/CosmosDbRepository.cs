using Microsoft.Azure.Cosmos;
using System.Runtime.CompilerServices;
using Terminal.Interfaces;
using Terminal.Models;

namespace TerminalCosmosDB
{
    public class CosmosDbRepository : IRepository<Product>, IDisposable
    {
        private readonly Container _container;
        private FeedIterator<Product>? _feedIterator;
        public CosmosDbRepository(Container container)
        {
            _container = container;
        }
        public IAsyncEnumerable<Product> GetAll()
        {
            _feedIterator = _container.GetItemQueryIterator<Product>(queryText: "SELECT * FROM products");
            return _feedIterator.AsAsyncEnumerable();
        }
        public async Task CreateAsync(Product product)
        {
            var cosmosProduct = new CosmosProduct(product.Code, product.Price, product.DiscountPrice, product.DiscountQuantity);
            await _container.UpsertItemAsync<CosmosProduct>(cosmosProduct);
        }
        public async Task UpdateAsync(Product product)
        {
            var cosmosProduct = new CosmosProduct(product.Code, product.Price, product.DiscountPrice, product.DiscountQuantity);
            await _container.UpsertItemAsync<CosmosProduct>(cosmosProduct);
        }
        public async Task<Product?> GetAsync(string code)
        {
            return await _container.ReadItemAsync<CosmosProduct>(code, new PartitionKey(code));
        }
        public async Task DeleteAsync(string code)
        {
            await _container.DeleteItemAsync<CosmosProduct>(code, new PartitionKey(code));
        }
        public void Dispose()
        {
            if (_feedIterator != null) 
                _feedIterator.Dispose();
            GC.SuppressFinalize(this);
        }
    }
    public static class FeedIteratorExtension
    {
        public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(
            this FeedIterator<T> iterator,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            while (iterator.HasMoreResults)
            {
                var page = await iterator.ReadNextAsync(cancellationToken);
                foreach (var item in page)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }
            }
            iterator.Dispose();
        }
    }
}
