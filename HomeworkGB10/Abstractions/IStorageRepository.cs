using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Abstractions
{
    public interface IStorageRepository : IRepository
    {
        public List<StorageDTO> GetStorages();
        public string GetStoragesCsv();
        public int AddStorage(StorageDTO storage);
        public int PutStorage(StorageDTO storage);
        public int UpdateQuantityAtStorage(int id, int quantity);
        public int DeleteStorage(int id);
    }
}
