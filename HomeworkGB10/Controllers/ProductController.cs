using HomeworkGB10.Abstractions;
using HomeworkGB10.DatabaseModel.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("api_storage/[controller]")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet(template: "get")]
        public ActionResult<List<GetProductDTO>> GetProducts()
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

        [HttpGet(template: "get_as_csv")]
        public ActionResult GetProductsCsv()
        {
            try
            {
                string result = _productRepository.GetProductsCsv();
                return File(Encoding.UTF8.GetBytes(result), "text/csv", "products_table.csv");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_as_url")]
        public ActionResult<string> GetProductsCsvUrl()
        {
            try
            {
                string fileName = _productRepository.GetProductsCsvUrl();
                if (fileName == string.Empty) return StatusCode(404);
                return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_cache_stats")]
        public ActionResult<MemoryCacheStatistics?> GetCacheStatistics()
        {
            return _productRepository.GetCacheStatistics();
        }

        [HttpGet(template: "get_cache_stats_as_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            string fileName = _productRepository.GetCacheStatisticsCsvUrl();
            if (fileName == string.Empty) return StatusCode(404);
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post")]
        public ActionResult<int> AddProduct([FromBody] PutProductDTO productDTO)
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

        [HttpPut(template: "put")]
        public ActionResult<int> PutProduct([FromBody] PutProductDTO productDTO)
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

        [HttpPatch(template: "patch/{id}")]
        public ActionResult<int> UpdatePriceOfProduct(int id, double price)
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

        [HttpDelete(template: "delete/{id}")]
        public ActionResult<int> DeleteProduct(int id)
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
