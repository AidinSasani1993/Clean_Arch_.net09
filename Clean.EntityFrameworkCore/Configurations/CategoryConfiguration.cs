using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.EntityFrameworkCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(c => c.Id);
            builder.Property(a => a.Title).HasMaxLength(Category.TitleMaxLength).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(Category.DescriptionMaxLength);
        }
    }
}
