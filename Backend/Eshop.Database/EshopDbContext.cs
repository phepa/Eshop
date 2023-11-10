using Eshop.Database.Configurations;
using Eshop.Database.Entities;
using Eshop.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Database
{
    public sealed class EshopDbContext : DbContext, IMigratable
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
