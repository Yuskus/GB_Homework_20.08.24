using AutoMapper;
using HomeworkGB10.Abstractions;
using HomeworkGB10.Models;
using HomeworkGB10.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Repo
{
    public class ProductRepository(IMapper mapper, IMemoryCache cache, IConfigurationRoot root) : IProductRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        private readonly string _connect = root.GetConnectionString("StoreDb")!;

        public MemoryCacheStatistics? GetCacheStatistics() => _cache.GetCurrentStatistics();

        public List<ProductDTO> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDTO>? products))
            {
                if (products is not null) return products;
            }
            using var context = new StorageContext(_connect);
            var result = context.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
            _cache.Set("products", products, TimeSpan.FromMinutes(30));
            return result;
        }

        public string GetProductsCsv()
        {
            List<ProductDTO> products = GetProducts();
            var sb = new StringBuilder();
            sb.AppendLine($"ID;Name;Description;Price;Category ID");
            for (int i = 0; i < products.Count; i++)
            {
                sb.AppendLine($"{products[i].Id};{products[i].Name};{products[i].Description};{products[i].Price};{products[i].CategoryId}");
            }
            return sb.ToString();
        }
        public int AddProduct(ProductDTO productDTO)
        {
            using var context = new StorageContext(_connect);
            var product = context.Products.FirstOrDefault(x => x.Name == productDTO.Name);
            if (product is null)
            {
                product = _mapper.Map<Product>(productDTO);
                product.Id = 0;
                context.Products.Add(product);
                context.SaveChanges();
                _cache.Remove("products");
            }
            return product.Id;
        }

        public int PutProduct(ProductDTO productDTO)
        {
            using var context = new StorageContext(_connect);
            var product = context.Products.FirstOrDefault(x => x.Id == productDTO.Id);
            if (product is null)
            {
                product = _mapper.Map<Product>(productDTO);
                context.Products.Add(product);
            }
            else
            {
                product.Id = productDTO.Id;
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.CategoryId = productDTO.CategoryId;
            }
            context.SaveChanges();
            _cache.Remove("products");
            return product.Id;
        }

        public int UpdatePriceOfProduct(int id, double price)
        {
            using var context = new StorageContext(_connect);
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return -1;
            product.Price = price;
            context.SaveChanges();
            _cache.Remove("products");
            return product.Id;
        }

        public int DeleteProduct(int id)
        {
            using var context = new StorageContext(_connect);
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return -1;
            context.Products.Remove(product);
            context.SaveChanges();
            _cache.Remove("products");
            return id;
        }
    }
}
