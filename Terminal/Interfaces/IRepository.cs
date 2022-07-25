namespace Terminal.Interfaces
{
    public interface IRepository<T> where T : class, IHasCode
    {
        public Task<T?> GetAsync(string code);
        public IAsyncEnumerable<T> GetAll();
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(string code);
    }
}
