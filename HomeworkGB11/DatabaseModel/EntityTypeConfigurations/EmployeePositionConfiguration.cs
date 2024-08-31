using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB11.DatabaseModel.EntityTypeConfigurations
{
    public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
    {
        public void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("emp_position_id_pk");

            builder.ToTable("employees_positions");

            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(128)
                   .HasColumnName("name");

            builder.Property(e => e.Description)
                   .HasMaxLength(500)
                   .HasColumnName("description");

            builder.Property(e => e.BaseSalary)
                   .HasColumnName("base_salary");

            builder.HasMany(e => e.Employees)
                   .WithOne(e => e.Position)
                   .HasForeignKey(e => e.PositionId)
                   .HasConstraintName("position_to_emp_id_fk");
        }
    }
}
