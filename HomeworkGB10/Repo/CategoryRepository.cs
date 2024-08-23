using HomeworkGB10.Models.DTO;
using HomeworkGB10.Models;
using HomeworkGB10.Abstractions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Repo
{
    public class CategoryRepository(IMapper mapper, IMemoryCache cache, IConfigurationRoot root) : ICategoryRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        private readonly string _connect = root.GetConnectionString("StoreDb")!;

        public MemoryCacheStatistics? GetCacheStatistics() => _cache.GetCurrentStatistics();

        public List<CategoryDTO> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDTO>? categories))
            {
                if (categories != null) return categories;
            }
            using var context = new StorageContext(_connect);
            var result = context.Categories.Select(x => _mapper.Map<CategoryDTO>(x)).ToList();
            _cache.Set("categories", result, TimeSpan.FromMinutes(30));
            return result;
        }

        public string GetCategoriesCsv()
        {
            List<CategoryDTO> categories = GetCategories();
            var sb = new StringBuilder();
            sb.AppendLine($"ID;Name");
            for (int i = 0; i < categories.Count; i++)
            {
                sb.AppendLine($"{categories[i].Id};{categories[i].Name}");
            }
            return sb.ToString();
        }

        public int AddCategory(CategoryDTO categoryDTO)
        {
            using var context = new StorageContext(_connect);
            var category = context.Categories.FirstOrDefault(x => x.Name == categoryDTO.Name);
            if (category is null)
            {
                category = _mapper.Map<Category>(categoryDTO);
                category.Id = 0;
                context.Categories.Add(category);
                context.SaveChanges();
                _cache.Remove("categories");
            }
            return category.Id;
        }
        public int PutCategory(CategoryDTO categoryDTO)
        {
            using var context = new StorageContext(_connect);
            var category = context.Categories.FirstOrDefault(x => x.Id == categoryDTO.Id);
            if (category is null)
            {
                category = _mapper.Map<Category>(categoryDTO);
                context.Categories.Add(category);
            }
            else
            {
                category.Id = categoryDTO.Id;
                category.Name = categoryDTO.Name;
            }
            context.SaveChanges();
            _cache.Remove("categories");
            return category.Id;
        }
        public int DeleteCategory(int id)
        {
            using var context = new StorageContext(_connect);
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return -1;
            context.Categories.Remove(category);
            context.SaveChanges();
            _cache.Remove("categories");
            return id;
        }
    }
}
