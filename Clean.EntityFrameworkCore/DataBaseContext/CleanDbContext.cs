using Clean.Domain.Entities;
using Clean.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Clean.EntityFrameworkCore.DataBaseContext
{
    public class CleanDbContext : DbContext
    {
        #region [-ctors-]
        public CleanDbContext(DbContextOptions options) : base(options)
        {
        }

        protected CleanDbContext()
        {
        } 
        #endregion

        #region [-Props-]
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

        #region [-OnModelCreating(ModelBuilder modelBuilder)-]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
        #endregion

    }
}
