using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeworkGB10.Models.EntityTypeConfigurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasKey(p => p.Id)
                      .HasName("id_storage_pk");

            builder.ToTable("storage");

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.ProductId)
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
