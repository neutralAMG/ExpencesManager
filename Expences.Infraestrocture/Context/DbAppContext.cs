

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
            this.ChangeTracker.LazyLoadingEnabled = true;   
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-QCIUVPFJ;Database=ExpencesManager;User ID=sa;Password=Alejandro23@#; TrustServerCertificate=true;");
        }
    }
}
