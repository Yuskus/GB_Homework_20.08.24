using HomeworkGB10.Abstractions;
using HomeworkGB10.DatabaseModel.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageShelfController(IStorageShelfRepository storageRepository) : ControllerBase
    {
        private readonly IStorageShelfRepository _storageRepository = storageRepository;

        [HttpGet(template: "get_storages")] 
        public ActionResult<List<GetStorageDTO>> GetStorages()
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
                string fileName = _storageRepository.GetStoragesCsvUrl();
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
            return _storageRepository.GetCacheStatistics();
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            string fileName = _storageRepository.GetCacheStatisticsCsvUrl();
            if (fileName == string.Empty) return StatusCode(404);
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPost(template: "post_storage")]
        public ActionResult<int> AddStorage([FromBody] PutStorageDTO storageDTO)
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
        public ActionResult<int> PutStorage([FromBody] PutStorageDTO storageDTO)
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
        public ActionResult<int> UpdateQuantityAtStorage(int id, int quantity)
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
        public ActionResult<int> DeleteStorage(int id)
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
