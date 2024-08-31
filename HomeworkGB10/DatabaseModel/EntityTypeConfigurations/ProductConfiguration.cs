using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeworkGB10.Models.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id)
                      .HasName("id_product_pk");

            builder.ToTable("products");

            builder.Property(p => p.Id).HasColumnName("id");

            builder.HasIndex(p => p.Name)
                  .IsUnique();

            builder.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(255)
                  .HasColumnName("name");

            builder.Property(p => p.Description)
                  .HasColumnName("description");

            builder.Property(p => p.Price)
                  .HasColumnType("decimal(10, 2)")
                  .HasColumnName("price");

            builder.Property(p => p.CategoryId)
                  .HasColumnName("category_id");

            builder.HasOne(p => p.Category)
                  .WithMany(p => p.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .HasConstraintName("category_id_fk");

            builder.HasMany(p => p.Storages)
                  .WithOne(p => p.Product)
                  .HasForeignKey(p => p.ProductId)
                  .HasConstraintName("storages_fk");
        }
    }
}
