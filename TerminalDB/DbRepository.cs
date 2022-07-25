using Microsoft.EntityFrameworkCore;
using Terminal.Interfaces;

namespace TerminalDB
{
    public class DbRepository<T> : IRepository<T> where T : class, IHasCode
    {
        private readonly ApplicationContext context;
        private readonly DbSet<T> entities;
        private const string errorMessage = "Entity is empty";
        public DbRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IAsyncEnumerable<T> GetAll()
        {
            return entities.AsAsyncEnumerable();
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new TerminalDbException(errorMessage);
            }
            entities.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new TerminalDbException(errorMessage);
            }
            entities.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task<T?> GetAsync(string code)
        {
            return await entities.FirstOrDefaultAsync(e => e.Code == code);
        }
        public async Task DeleteAsync(string code)
        {
            var entity = await GetAsync(code);
            if (entity == null)
            {
                throw new TerminalDbException($"Could not find entity with code = {code}");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
