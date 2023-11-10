using Eshop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Database.Configurations
{
    public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Id).HasComment("Product ID");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("Product name");
            builder.Property(e => e.CreatedOnUtc).IsRequired().HasComment("Date when product was created");
            builder.Property(e => e.UpdatedOnUtc).HasComment("Date when product was last updated");

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Product_Category");

            OnConfigurePartial(builder);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> entity);
    }
}
