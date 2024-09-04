using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB11.DatabaseModel
{
    public class EmployeesDbContext : DbContext, IEmployeesDbContext
    {
        private readonly string? _connectionString;
        public virtual DbSet<EmployeeInfo> Employees { get; set; }
        public virtual DbSet<EmployeePosition> EmployeesPosition { get; set; }
        public virtual DbSet<WorkZone> WorkZones { get; set; }
        public EmployeesDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeInfoConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePositionConfiguration());
            modelBuilder.ApplyConfiguration(new WorkZoneConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
