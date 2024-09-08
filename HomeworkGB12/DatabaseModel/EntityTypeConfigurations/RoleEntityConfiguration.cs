using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB12.DatabaseModel.EntityTypeConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(p => p.Id)
                   .HasName("pk_role_id");

            builder.ToTable("roles");

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.HasIndex(p => p.Name)
                   .IsUnique();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("name");

            builder.HasMany(p => p.Users)
                   .WithOne(p => p.Role)
                   .HasForeignKey(p => p.RoleId)
                   .HasConstraintName("fk_role_user_id");
        }
    }
}
