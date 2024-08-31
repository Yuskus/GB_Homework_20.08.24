using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB11.DatabaseModel.EntityTypeConfigurations
{
    public class EmployeeInfoConfiguration : IEntityTypeConfiguration<EmployeeInfo>
    {
        public void Configure(EntityTypeBuilder<EmployeeInfo> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("employee_id_pk");

            builder.ToTable("employees_list");

            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(128)
                   .HasColumnName("name");

            builder.Property(e => e.Surname)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("surname");

            builder.Property(e => e.Patronymic)
                   .HasMaxLength(255)
                   .HasColumnName("patronymic");

            builder.Property(e => e.Birthday)
                   .HasColumnName("birthday");

            builder.Property(e => e.HiringDate)
                   .HasColumnName("hiring_date");

            builder.Property(e => e.Adress)
                   .IsRequired()
                   .HasMaxLength(500)
                   .HasColumnName("home_adress");

            builder.Property(e => e.Phone)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasColumnName("phone_number");

            builder.Property(e => e.WorkZoneId)
                   .HasColumnName("workzone_id");

            builder.Property(e => e.PositionId)
                   .HasColumnName("emp_position_id");

            builder.HasOne(e => e.WorkZone)
                   .WithMany(e => e.Employees)
                   .HasForeignKey(e => e.WorkZoneId)
                   .HasConstraintName("workzone_id_fk");

            builder.HasOne(e => e.Position)
                   .WithMany(e => e.Employees)
                   .HasForeignKey(e => e.PositionId)
                   .HasConstraintName("emp_position_id_fk");
        }
    }
}
