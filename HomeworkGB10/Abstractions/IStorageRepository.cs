using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Abstractions
{
    public interface IStorageRepository : IRepository
    {
        public List<GetStorageDTO> GetStorages();
        public string GetStoragesCsv();
        public string GetStoragesCsvUrl();
        public int AddStorage(PutStorageDTO storage);
        public int PutStorage(PutStorageDTO storage);
        public int UpdateQuantityAtStorage(int id, int quantity);
        public int DeleteStorage(int id);
    }
}
