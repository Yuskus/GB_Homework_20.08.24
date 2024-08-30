namespace HomeworkGB10.Models
{
    public class DbInitializer
    {
        public static void Initialize(StorageDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
