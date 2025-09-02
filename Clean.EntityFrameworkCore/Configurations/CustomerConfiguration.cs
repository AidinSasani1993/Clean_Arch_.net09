using Clean.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.EntityFrameworkCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FirstName).HasMaxLength(Customer.FirstNameMinLength).IsRequired();
        }
    }
}
