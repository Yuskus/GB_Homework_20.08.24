using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeworkGB10.DatabaseModel.EntityTypeConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id)
                   .HasName("id_category_pk");

            builder.ToTable("category");

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.HasIndex(p => p.Name)
                   .IsUnique();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("name");

            builder.HasMany(p => p.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .HasConstraintName("products_fk");
        }
    }
}
