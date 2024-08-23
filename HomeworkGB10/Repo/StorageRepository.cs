using AutoMapper;
using HomeworkGB10.Abstractions;
using HomeworkGB10.Models;
using HomeworkGB10.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Repo
{
    public class StorageRepository(IMapper mapper, IMemoryCache cache, IConfigurationRoot root) : IStorageRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        private readonly string _connect = root.GetConnectionString("StoreDb")!;

        public MemoryCacheStatistics? GetCacheStatistics() => _cache.GetCurrentStatistics();

        public List<StorageDTO> GetStorages()
        {
            if (_cache.TryGetValue("storages", out List<StorageDTO>? storages))
            {
                if (storages != null) return storages;
            }
            using var context = new StorageContext(_connect);
            var result = context.Storages.Select(x => _mapper.Map<StorageDTO>(x)).ToList();
            _cache.Set("storages", storages, TimeSpan.FromMinutes(30));
            return result;
        }

        public string GetStoragesCsv()
        {
            List<StorageDTO> storages = GetStorages();
            var sb = new StringBuilder();
            sb.AppendLine($"ID;Product ID;Quantity");
            for (int i = 0; i < storages.Count; i++)
            {
                sb.AppendLine($"{storages[i].Id};{storages[i].ProductId};{storages[i].Quantity}");
            }
            return sb.ToString();
        }

        public int AddStorage(StorageDTO storageDTO)
        {
            using var context = new StorageContext(_connect);
            var storage = context.Storages.FirstOrDefault(x => x.ProductId == storageDTO.ProductId);
            if (storage is null)
            {
                storage = _mapper.Map<Storage>(storageDTO);
                storage.Id = 0;
                context.Storages.Add(storage);
                context.SaveChanges();
                _cache.Remove("storages");
            }
            return storage.Id;
        }

        public int PutStorage(StorageDTO storageDTO)
        {
            using var context = new StorageContext(_connect);
            var storage = context.Storages.FirstOrDefault(x => x.Id == storageDTO.Id);
            if (storage is null)
            {
                storage = _mapper.Map<Storage>(storageDTO);
                context.Storages.Add(storage);
            }
            else
            {
                storage.Id = storageDTO.Id;
                storage.ProductId = storageDTO.ProductId;
                storage.Quantity = storageDTO.Quantity;
            }
            context.SaveChanges();
            _cache.Remove("storages");
            return storage.Id;
        }

        public int UpdateQuantityAtStorage(int id, int quantity)
        {
            using var context = new StorageContext(_connect);
            var storage = context.Storages.FirstOrDefault(x => x.Id == id);
            if (storage is null) return -1;
            storage.Quantity = quantity;
            context.SaveChanges();
            _cache.Remove("storages");
            return id;
        }

        public int DeleteStorage(int id)
        {
            using var context = new StorageContext(_connect);
            var storage = context.Storages.FirstOrDefault(x => x.Id == id);
            if (storage is null) return -1;
            context.Storages.Remove(storage);
            context.SaveChanges();
            _cache.Remove("storages");
            return id;
        }
    }
}
