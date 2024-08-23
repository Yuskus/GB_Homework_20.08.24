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
    public class StorageController(IStorageRepository storageRepository) : ControllerBase
    {
        private readonly IStorageRepository _storageRepository = storageRepository;

        [HttpGet(template: "get_storages")] 
        public ActionResult GetStorages()
        {
            try
            {
                var storages = _storageRepository.GetStorages();
                return Ok(storages);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_storages_csv")] 
        public ActionResult GetStoragesCsv()
        {
            try
            {
                string result = _storageRepository.GetStoragesCsv();
                return File(Encoding.UTF8.GetBytes(result), "text/csv", "storages_table.csv");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "get_storages_csv_url")]
        public ActionResult<string> GetCategoriesCsvUrl()
        {
            try
            {
                var result = _storageRepository.GetStoragesCsv();
                if (result == null) return StatusCode(404);
                string fileName = $"storages_table_{DateTime.Now:yyyyMMddHHmmss}.csv";
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
            return _storageRepository.GetCacheStatistics();
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            var statistics = _storageRepository.GetCacheStatistics();
            if (statistics == null) return StatusCode(404);
            var sb = new StringBuilder();
            sb.AppendLine("\"Storage\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"storages_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            System.IO.File.WriteAllText(path, sb.ToString());
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post_storage")]
        public ActionResult AddStorage([FromBody] StorageDTO storageDTO)
        {
            try
            {
                int resultIndex = _storageRepository.AddStorage(storageDTO);
                return Ok(resultIndex);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "put_storage")]
        public ActionResult PutStorage([FromBody] StorageDTO storageDTO)
        {
            try
            {
                int result = _storageRepository.PutStorage(storageDTO);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "patch_storage/{id}")]
        public ActionResult UpdateQuantityAtStorage(int id, int quantity)
        {
            try
            {
                int result = _storageRepository.UpdateQuantityAtStorage(id, quantity);
                if (result < 0) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete_storage/{id}")]
        public ActionResult DeleteStorage(int id)
        {
            try
            {
                int result = _storageRepository.DeleteStorage(id);
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
