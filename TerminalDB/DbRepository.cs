using Microsoft.EntityFrameworkCore;
using Terminal.Interfaces;

namespace TerminalDB
{
    public class DbRepository<T> : IRepository<T> where T : class, IHasCode
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _entities;
        private const string errorMessage = "Entity is empty";
        public DbRepository(ApplicationContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IAsyncEnumerable<T> GetAll()
        {
            return _entities.AsAsyncEnumerable();
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new TerminalDbException(errorMessage);
            }
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new TerminalDbException(errorMessage);
            }
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<T?> GetAsync(string code)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Code == code);
        }
        public async Task DeleteAsync(string code)
        {
            var entity = await GetAsync(code);
            if (entity == null)
            {
                throw new TerminalDbException($"Could not find entity with code = {code}");
            }
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
