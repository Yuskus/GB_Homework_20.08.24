using Microsoft.EntityFrameworkCore;

namespace HomeworkGB10.Models
{
    public class StorageContext(string connectionString) : DbContext
    {
        private readonly string _connectionString = connectionString;
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id)
                      .HasName("id_product_pk");

                entity.ToTable("products");

                entity.Property(p => p.Id).HasColumnName("id");

                entity.HasIndex(p => p.Name)
                      .IsUnique();

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("name");

                entity.Property(p => p.Description)
                      .HasColumnName("description");

                entity.Property(p => p.Price)
                      .HasColumnType("decimal(10, 2)")
                      .HasColumnName("price");

                entity.Property(p => p.CategoryId)
                      .HasColumnName("category_id");

                entity.HasOne(p => p.Category)
                      .WithMany(p => p.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .HasConstraintName("category_id_fk");

                entity.HasMany(p => p.Storages)
                      .WithOne(p => p.Product)
                      .HasForeignKey(p => p.ProductId)
                      .HasConstraintName("storages_fk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(p => p.Id)
                      .HasName("id_category_pk");

                entity.ToTable("category");

                entity.Property(p => p.Id)
                      .HasColumnName("id");

                entity.HasIndex(p => p.Name)
                      .IsUnique();

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("name");

                entity.HasMany(p => p.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .HasConstraintName("products_fk");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(p => p.Id)
                      .HasName("id_storage_pk");

                entity.ToTable("storage");

                entity.Property(p => p.Id).HasColumnName("id");

                entity.Property(p => p.ProductId)
                      .HasColumnName("product_id");

                entity.Property(p => p.Quantity)
                      .HasColumnName("quantity");

                entity.HasOne(p => p.Product)
                      .WithMany(p => p.Storages)
                      .HasForeignKey(p => p.ProductId)
                      .HasConstraintName("product_fk");
            });
        }
    }
}
