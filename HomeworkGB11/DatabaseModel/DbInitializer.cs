namespace HomeworkGB11.DatabaseModel
{
    public class DbInitializer
    {
        public static void Initialize(EmployeesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
