using Clean.Domain.Entities;
using Clean.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.EntityFrameworkCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(c => c.Id);
            builder.HasOne(a => a.Role).WithMany(a => a.Users)
                .HasForeignKey(a => a.RoleRef);
        }
    }
}
