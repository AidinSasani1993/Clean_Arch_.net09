using Clean.Domain.Entities;
using Clean.Domain.Entities.Customers;
using Clean.Domain.Entities.Users;
using Clean.EntityFrameworkCore.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.EntityFrameworkCore.DataBaseContext
{
    public class CleanDbContext : IdentityDbContext<User>
    {
        public CleanDbContext(DbContextOptions options) : base(options)
        {
        }

        #region [-ctors-]


        protected CleanDbContext()
        {
        } 
        #endregion

        #region [-Props-]
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        #endregion

        #region [-OnModelCreating(ModelBuilder modelBuilder)-]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
        #endregion

    }
}
