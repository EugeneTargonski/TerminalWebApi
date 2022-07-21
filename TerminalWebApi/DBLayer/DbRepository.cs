using Microsoft.EntityFrameworkCore;
using TerminalWebApi.Interfaces;

namespace TerminalWebApi.DBLayer
{
    public class DbRepository<T> : IRepository<T> where T : class, IHasId, IHasCode
    {
        private readonly ApplicationContext context;
        private readonly DbSet<T> entities;
        string errorMessage = string.Empty;
        public DbRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IAsyncEnumerable<T> GetAll()
        {
            return entities.AsAsyncEnumerable();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await entities.FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException($"Could not find entity with id = {id}");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T?> GetByCodeAsync(string code)
        {
            return await entities.FirstOrDefaultAsync(e => e.Code == code);
        }

        public async Task DeleteAsync(string code)
        {
            var entity = await GetByCodeAsync(code);
            if (entity == null)
            {
                throw new ArgumentNullException($"Could not find entity with code = {code}");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
