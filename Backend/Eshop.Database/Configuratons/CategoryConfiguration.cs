using Eshop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Database.Configurations
{
    public partial class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Id).HasComment("Product ID");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("Product name");
            builder.Property(e => e.CreatedOnUtc).IsRequired().HasComment("Date when product was created");
            builder.Property(e => e.UpdatedOnUtc).HasComment("Date when product was last updated");

            OnConfigurePartial(builder);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Category> entity);
    }
}
