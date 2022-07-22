using Microsoft.EntityFrameworkCore;
using Terminal.Models;

namespace TerminalWebApi.DBLayer
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(p => p.Code).IsUnique();

            modelBuilder.Entity<Product>().HasData(
                    new Product ("A", 1.25, 3, 3) { Id = 1 },
                    new Product ("B", 4.25) { Id = 2 },
                    new Product ("C", 1, 5, 6) { Id = 3 }, 
                    new Product ("D", 0.75) { Id = 4 });
        }
    }
}
