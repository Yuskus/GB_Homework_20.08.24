using HomeworkGB10.Models.DTO;
using HomeworkGB10.Models;
using HomeworkGB10.Abstractions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Repo
{
    public class CategoryRepository(IMapper mapper, IMemoryCache cache, IStorageDbContext storageContext) : ICategoryRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        private readonly IStorageDbContext _storageContext = storageContext;

        public List<GetCategoryDTO> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<GetCategoryDTO>? categories))
            {
                if (categories != null) return categories;
            }
            var result = _storageContext.Categories.Select(x => _mapper.Map<GetCategoryDTO>(x)).ToList();
            _cache.Set("categories", result, TimeSpan.FromMinutes(30));
            return result;
        }

        public string GetCategoriesCsv()
        {
            List<GetCategoryDTO> categories = GetCategories();
            var sb = new StringBuilder();
            sb.AppendLine($"ID;Name");
            for (int i = 0; i < categories.Count; i++)
            {
                sb.AppendLine($"{categories[i].Id};{categories[i].Name}");
            }
            return sb.ToString();
        }

        public string GetCategoriesCsvUrl()
        {
            var result = GetCategoriesCsv();
            if (result is null) return string.Empty;
            string fileName = $"categories_table_{DateTime.Now:yyyyMMddHHmmss}.csv";
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
            sb.AppendLine("\"Category\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"categories_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            File.WriteAllText(path, sb.ToString());
            return fileName;
        }

        public int AddCategory(PutCategoryDTO categoryDTO)
        {
            var category = _storageContext.Categories.FirstOrDefault(x => x.Name == categoryDTO.Name);
            if (category is null)
            {
                category = _mapper.Map<Category>(categoryDTO);
                _storageContext.Categories.Add(category);
                _storageContext.SaveChanges();
                _cache.Remove("categories");
            }
            return category.Id;
        }
        public int UpdateCategory(int id, string name)
        {
            var category = _storageContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return -1;
            category.Name = name;
            _storageContext.SaveChanges();
            _cache.Remove("categories");
            return category.Id;
        }
        public int DeleteCategory(int id)
        {
            var category = _storageContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return -1;
            _storageContext.Categories.Remove(category);
            _storageContext.SaveChanges();
            _cache.Remove("categories");
            return id;
        }
    }
}
