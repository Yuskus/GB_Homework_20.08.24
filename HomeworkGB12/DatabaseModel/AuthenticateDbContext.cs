using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB12.DatabaseModel
{
    public class AuthenticateDbContext(string connectionString) : DbContext, IAuthenticateDbContext
    {
        private readonly string? _connectionString = connectionString;
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
