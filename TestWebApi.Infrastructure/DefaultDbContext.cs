
using Microsoft.EntityFrameworkCore;

using TestWebApi.Infrastructure.Models;
using TestWebApi.Infrastructure.Repositories.Configurations;

namespace TestWebApi.Infrastructure
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options) { }

        public DbSet<SomeModel> SomeModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SomeModelConfiguration());
        }
    }
}
