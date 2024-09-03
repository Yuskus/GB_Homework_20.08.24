using HomeworkGB10.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB10.Abstractions
{
    public interface IStorageDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<StorageShelf> Storages { get; set; }
        int SaveChanges();
    }
}
