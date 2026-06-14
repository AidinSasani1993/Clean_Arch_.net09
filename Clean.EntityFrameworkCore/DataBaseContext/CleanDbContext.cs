using Clean.Domain.Entities;
using Clean.Domain.Entities.Customers;
using Clean.Domain.Entities.Files;
using Clean.Domain.Entities.Roles;
using Clean.Domain.Entities.Users;
using Clean.EntityFrameworkCore.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.EntityFrameworkCore.DataBaseContext
{
    public class CleanDbContext : DbContext
    {
        #region [-ctors-]
        public CleanDbContext(DbContextOptions<CleanDbContext> options) : base(options)
        {
        }

        protected CleanDbContext()
        {
        } 
        #endregion

        #region [-Props-]
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        #endregion

        #region [-OnModelCreating(ModelBuilder modelBuilder)-]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
        #endregion

    }
}
