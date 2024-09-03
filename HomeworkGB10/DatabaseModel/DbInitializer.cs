namespace HomeworkGB10.DatabaseModel
{
    public class DbInitializer
    {
        public static void Initialize(StorageDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
