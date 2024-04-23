

using Expences.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expences.Infraestrocture.Context
{
    public class DbAppContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Expences.Domain.Entities.Expences> Expences { get; set; }
        public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("ConectionString");
        }
    }
}
