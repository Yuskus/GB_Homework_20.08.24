using HomeworkGB11.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB11.Abstractions
{
    public interface IEmployeesDbContext
    {
        DbSet<EmployeeInfo> Employees { get; set; }
        DbSet<EmployeePosition> EmployeesPosition { get; set; }
        DbSet<WorkZone> WorkZones { get; set; }
        int SaveChanges();
    }
}
