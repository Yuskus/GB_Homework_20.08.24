using HomeworkGB10.Abstractions;
using HomeworkGB10.DatabaseModel.DTO;
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
                string fileName = _categoryRepository.GetCategoriesCsvUrl();
                if (fileName == string.Empty) return StatusCode(404);
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
            string fileName = _categoryRepository.GetCacheStatisticsCsvUrl();
            if (fileName == string.Empty) return StatusCode(404);
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post_category")]
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
        [HttpPatch(template: "put_category/{id}")]
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

        [HttpDelete(template: "delete_category/{id}")]
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
