using HomeworkGB10.Abstractions;
using HomeworkGB10.DatabaseModel.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("api_storage/[controller]")]
    public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        [HttpGet(template: "get")]
        public ActionResult<List<GetCategoryDTO>> GetCategories()
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

        [HttpGet(template: "get_as_csv")]
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

        [HttpGet(template: "get_as_url")]
        public ActionResult<string> GetCategoriesCsvUrl()
        {
            try
            {
                string fileName = _categoryRepository.GetCategoriesCsvUrl();
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
            return _categoryRepository.GetCacheStatistics();
        }

        [HttpGet(template: "get_cache_stats_as_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            string fileName = _categoryRepository.GetCacheStatisticsCsvUrl();
            if (fileName == string.Empty) return StatusCode(404);
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post")]
        public ActionResult<int> AddCategory([FromBody] PutCategoryDTO categoryDTO)
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
        [HttpPatch(template: "patch/{id}")]
        public ActionResult<int> UpdateCategory(int id, string name)
        {
            try
            {
                int result = _categoryRepository.UpdateCategory(id, name);
                if (result < 0) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete/{id}")]
        public ActionResult<int> DeleteCategory(int id)
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
