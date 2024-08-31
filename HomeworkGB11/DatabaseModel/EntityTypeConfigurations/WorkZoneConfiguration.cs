using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB11.DatabaseModel.EntityTypeConfigurations
{
    public class WorkZoneConfiguration : IEntityTypeConfiguration<WorkZone>
    {
        public void Configure(EntityTypeBuilder<WorkZone> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("workzone_id_pk");

            builder.ToTable("work_zones");

            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.ZoneName)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("region_name");

            builder.HasMany(e => e.Employees)
                   .WithOne(e => e.WorkZone)
                   .HasForeignKey(e => e.WorkZoneId)
                   .HasConstraintName("workzone_to_emp_id_fk");
        }
    }
}
