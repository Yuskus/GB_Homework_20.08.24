using AutoMapper;
using HomeworkGB10.Abstractions;
using HomeworkGB10.Models;
using HomeworkGB10.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Repo
{
    public class ProductRepository(IMapper mapper, IMemoryCache cache, IStorageDbContext storageContext) : IProductRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        private readonly IStorageDbContext _storageContext = storageContext;

        public List<GetProductDTO> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<GetProductDTO>? products))
            {
                if (products is not null) return products;
            }
            var result = _storageContext.Products.Select(x => _mapper.Map<GetProductDTO>(x)).ToList();
            _cache.Set("products", products, TimeSpan.FromMinutes(30));
            return result;
        }

        public string GetProductsCsv()
        {
            List<GetProductDTO> products = GetProducts();
            var sb = new StringBuilder();
            sb.AppendLine($"ID;Name;Description;Price;Category ID");
            for (int i = 0; i < products.Count; i++)
            {
                sb.AppendLine($"{products[i].Id};{products[i].Name};{products[i].Description};{products[i].Price};{products[i].CategoryId}");
            }
            return sb.ToString();
        }

        public string GetProductsCsvUrl()
        {
            var result = GetProductsCsv();
            if (result is null) return string.Empty;
            string fileName = $"products_table_{DateTime.Now:yyyyMMddHHmmss}.csv";
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
            sb.AppendLine("\"Product\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"storages_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            File.WriteAllText(path, sb.ToString());
            return fileName;
        }

        public int AddProduct(PutProductDTO productDTO)
        {
            var product = _storageContext.Products.FirstOrDefault(x => x.Name == productDTO.Name);
            if (product is null)
            {
                product = _mapper.Map<Product>(productDTO);
                _storageContext.Products.Add(product);
                _storageContext.SaveChanges();
                _cache.Remove("products");
            }
            return product.Id;
        }

        public int PutProduct(PutProductDTO productDTO)
        {
            var product = _storageContext.Products.FirstOrDefault(x => x.Name == productDTO.Name);
            if (product is null)
            {
                product = _mapper.Map<Product>(productDTO);
                _storageContext.Products.Add(product);
            }
            else
            {
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.CategoryId = productDTO.CategoryId;
            }
            _storageContext.SaveChanges();
            _cache.Remove("products");
            return product.Id;
        }

        public int UpdatePriceOfProduct(int id, double price)
        {
            var product = _storageContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return -1;
            product.Price = price;
            _storageContext.SaveChanges();
            _cache.Remove("products");
            return product.Id;
        }

        public int DeleteProduct(int id)
        {
            var product = _storageContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return -1;
            _storageContext.Products.Remove(product);
            _storageContext.SaveChanges();
            _cache.Remove("products");
            return id;
        }
    }
}
