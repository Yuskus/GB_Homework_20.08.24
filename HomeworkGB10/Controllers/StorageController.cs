using HomeworkGB10.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        [HttpGet(template: "get_storages")] //storage
        public ActionResult GetStorages()
        {
            try
            {
                using var context = new StorageContext();
                var storages = context.Storages.Select(x => new { x.Id, x.ProductId, x.Quantity }).ToList();
                if (storages is null)
                {
                    return StatusCode(404);
                }
                return Ok(storages);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost(template: "post_storage")]
        public ActionResult AddStorage(int productId, int quantity)
        {
            try
            {
                using var context = new StorageContext();
                if (!context.Storages.Any(x => x.ProductId == productId))
                {
                    var storage = new Storage()
                    {
                        ProductId = productId,
                        Quantity = quantity
                    };
                    context.Storages.Add(storage);
                    context.SaveChanges();
                    var result = new { storage.Id, productId, quantity, Status = "Added" };
                    return Ok(result);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "put_storage")]
        public ActionResult PutStorage(int id, int productId, int quantity)
        {
            try
            {
                using var context = new StorageContext();
                var storage = context.Storages.FirstOrDefault(x => x.Id == id);
                if (storage is null)
                {
                    var createStorage = new Storage()
                    {
                        ProductId = productId,
                        Quantity = quantity
                    };
                    context.Storages.Add(createStorage);
                    context.SaveChanges();
                    var result = new { createStorage.Id, productId, quantity, Status = "Added" };
                    return Ok(result);
                }
                else
                {
                    storage.ProductId = productId;
                    storage.Quantity = quantity;
                    context.SaveChanges();
                    var result = new { storage.Id, productId, quantity, Status = "Changed" };
                    return Ok(result);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "patch_storage")]
        public ActionResult UpdateQuantityAtStorage(int id, int quantity)
        {
            try
            {
                using var context = new StorageContext();
                var storage = context.Storages.FirstOrDefault(x => x.Id == id);
                if (storage is null)
                {
                    return StatusCode(404);
                }
                storage.Quantity = quantity;
                context.SaveChanges();
                var result = new { id, quantity, Status = "Changed" };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete_storage")]
        public ActionResult DeleteStorage(int id)
        {
            try
            {
                using var context = new StorageContext();
                var storage = context.Storages.FirstOrDefault(x => x.Id == id);
                if (storage is null)
                {
                    return StatusCode(404);
                }
                var result = new { storage.Id, storage.ProductId, storage.Quantity, Status = "Deleted" };
                context.Storages.Remove(storage);
                context.SaveChanges();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
