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

        public List<GetStorageDTO> GetStorages()
        {
            if (_cache.TryGetValue("storages", out List<GetStorageDTO>? storages))
            {
                if (storages != null) return storages;
            }
            using var context = new StorageContext(_connect);
            var result = context.Storages.Select(x => _mapper.Map<GetStorageDTO>(x)).ToList();
            _cache.Set("storages", storages, TimeSpan.FromMinutes(30));
            return result;
        }

        public string GetStoragesCsv()
        {
            List<GetStorageDTO> storages = GetStorages();
            var sb = new StringBuilder();
            sb.AppendLine($"ID;Product ID;Quantity");
            for (int i = 0; i < storages.Count; i++)
            {
                sb.AppendLine($"{storages[i].Id};{storages[i].ProductId};{storages[i].Quantity}");
            }
            return sb.ToString();
        }

        public string GetStoragesCsvUrl()
        {
            var result = GetStoragesCsv();
            if (result == null) return string.Empty;
            string fileName = $"storages_table_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            File.WriteAllText(path, result);
            return fileName;
        }

        public MemoryCacheStatistics? GetCacheStatistics()
        {
            return _cache.GetCurrentStatistics();
        }

        public string GetCacheStatisticsCsvUrl()
        {
            var statistics = GetCacheStatistics();
            if (statistics == null) return string.Empty;
            var sb = new StringBuilder();
            sb.AppendLine("\"Storage\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"storages_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            File.WriteAllText(path, sb.ToString());
            return fileName;
        }

        public int AddStorage(PutStorageDTO storageDTO)
        {
            using var context = new StorageContext(_connect);
            var storage = context.Storages.FirstOrDefault(x => x.ProductId == storageDTO.ProductId);
            if (storage is null)
            {
                storage = _mapper.Map<Storage>(storageDTO);
                context.Storages.Add(storage);
                context.SaveChanges();
                _cache.Remove("storages");
            }
            return storage.Id;
        }

        public int PutStorage(PutStorageDTO storageDTO)
        {
            using var context = new StorageContext(_connect);
            var storage = context.Storages.FirstOrDefault(x => x.ProductId == storageDTO.ProductId);
            if (storage is null)
            {
                storage = _mapper.Map<Storage>(storageDTO);
                context.Storages.Add(storage);
            }
            else
            {
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
