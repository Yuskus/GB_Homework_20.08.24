using HomeworkGB10.Abstractions;
using HomeworkGB10.Models.DTO;
using HomeworkGB10.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        [HttpGet(template: "get_categories")]
        public ActionResult GetCategories()
        {
            try
            {
                var result = _categoryRepository.GetCategories();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_categories_csv")]
        public ActionResult GetCategoriesCsv()
        {
            try
            {
                var result = _categoryRepository.GetCategoriesCsv();
                return File(Encoding.UTF8.GetBytes(result), "text/csv", "category_table.csv");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_categories_csv_url")]
        public ActionResult<string> GetCategoriesCsvUrl()
        {
            try
            {
                var result = _categoryRepository.GetCategoriesCsv();
                if (result == null) return StatusCode(404);
                string fileName = $"categories_table_{DateTime.Now:yyyyMMddHHmmss}.csv";
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
            return _categoryRepository.GetCacheStatistics();
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            var statistics = _categoryRepository.GetCacheStatistics();
            if (statistics == null) return StatusCode(404);
            var sb = new StringBuilder();
            sb.AppendLine("\"Category\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"categories_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            System.IO.File.WriteAllText(path, sb.ToString());
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post_category")]
        public ActionResult AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                int result = _categoryRepository.AddCategory(categoryDTO);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut(template: "put_category")]
        public ActionResult PutCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                int result = _categoryRepository.PutCategory(categoryDTO);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete_category/{id}")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                int result = _categoryRepository.DeleteCategory(id);
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
