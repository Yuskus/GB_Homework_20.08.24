using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeworkGB10.DatabaseModel.EntityTypeConfigurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<StorageShelf>
    {
        public void Configure(EntityTypeBuilder<StorageShelf> builder)
        {
            builder.HasKey(p => p.Id)
                   .HasName("id_storage_pk");

            builder.ToTable("storage");

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.Property(p => p.ProductId)
                   .IsRequired()
                   .HasColumnName("product_id");

            builder.Property(p => p.Quantity)
                   .HasColumnName("quantity");

            builder.HasOne(p => p.Product)
                   .WithMany(p => p.Storages)
                   .HasForeignKey(p => p.ProductId)
                   .HasConstraintName("product_fk");
        }
    }
}
