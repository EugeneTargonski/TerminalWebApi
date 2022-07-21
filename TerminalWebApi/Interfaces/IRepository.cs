namespace TerminalWebApi.Interfaces
{
    public interface IRepository<T> where T : class, IHasId, IHasCode
    {
        public Task<T?> GetAsync(int id);
        public Task<T?> GetByCodeAsync(string code);
        public IAsyncEnumerable<T> GetAll();
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task DeleteAsync(string code);
    }
}
