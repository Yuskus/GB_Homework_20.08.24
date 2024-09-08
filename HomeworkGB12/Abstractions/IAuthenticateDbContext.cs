using HomeworkGB12.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB12.Abstractions
{
    public interface IAuthenticateDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        int SaveChanges();
    }
}
