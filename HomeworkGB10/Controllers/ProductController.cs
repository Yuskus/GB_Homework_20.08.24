using HomeworkGB10.Abstractions;
using HomeworkGB10.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet(template: "get_products")]
        public ActionResult GetProducts()
        {
            try
            {
                var products = _productRepository.GetProducts();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_products_csv")]
        public ActionResult GetProductsCsv()
        {
            try
            {
                var result = _productRepository.GetProductsCsv();
                return File(Encoding.UTF8.GetBytes(result), "text/csv", "products_table.csv");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_products_csv_url")]
        public ActionResult<string> GetProductsCsvUrl()
        {
            try
            {
                var result = _productRepository.GetProductsCsv();
                if (result == null) return StatusCode(404);
                string fileName = $"products_table_{DateTime.Now:yyyyMMddHHmmss}.csv";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
                System.IO.File.WriteAllText(path, result);
                return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_cache_statistics")]
        public ActionResult<MemoryCacheStatistics?> GetCacheStatistics()
        {
            return _productRepository.GetCacheStatistics();
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            var statistics = _productRepository.GetCacheStatistics();
            if (statistics == null) return StatusCode(404);
            var sb = new StringBuilder();
            sb.AppendLine("\"Product\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"storages_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            System.IO.File.WriteAllText(path, sb.ToString());
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post_product")]
        public ActionResult AddProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                int resultIndex = _productRepository.AddProduct(productDTO);
                return Ok(resultIndex);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "put_product")]
        public ActionResult PutProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                int result = _productRepository.PutProduct(productDTO);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "patch_product/{id}")]
        public ActionResult UpdatePriceOfProduct(int id, double price)
        {
            try
            {
                int result = _productRepository.UpdatePriceOfProduct(id, price);
                if (result < 0) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete_product/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                int result = _productRepository.DeleteProduct(id);
                if (result < 0) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
