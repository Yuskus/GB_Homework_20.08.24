using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB12.DatabaseModel.EntityTypeConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(p => p.Id)
                   .HasName("pk_user_id");

            builder.ToTable("users");

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.HasIndex(p => p.Username)
                   .IsUnique();

            builder.Property(p => p.Username)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("username");

            builder.Property(p => p.Password)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("password");

            builder.Property(p => p.Salt)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("salt");

            builder.HasOne(p => p.Role)
                   .WithMany(p => p.Users)
                   .HasForeignKey(p => p.RoleId)
                   .HasConstraintName("fk_user_role_id");
        }
    }
}
