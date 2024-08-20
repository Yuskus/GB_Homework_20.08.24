using HomeworkGB10.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet(template: "get_categories")]
        public ActionResult GetCategories()
        {
            try
            {
                using var context = new StorageContext();
                var categories = context.Categories.Select(x => new {x.Id, x.Name}).ToList();
                if (categories is null)
                {
                    return StatusCode(404);
                }
                return Ok(categories);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost(template: "post_category")]
        public ActionResult AddCategory(string name)
        {
            try
            {
                using var context = new StorageContext();
                if (!context.Categories.Any(x => x.Name == name))
                {
                    var category = new Category() { Name = name };
                    context.Categories.Add(category);
                    context.SaveChanges();
                    var result = new { category.Id, name, Status = "Added" };
                    return Ok(result);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut(template: "put_category")]
        public ActionResult PutCategory(int id, string name)
        {
            try
            {
                using var context = new StorageContext();
                var category = context.Categories.FirstOrDefault(x => x.Id == id);
                if (category is null)
                {
                    var createCategory = new Category() { Name = name };
                    context.Categories.Add(createCategory);
                    context.SaveChanges();
                    var result = new { createCategory.Id, createCategory.Name, Status = "Added" };
                    return Ok(result);
                }
                else
                {
                    category.Name = name;
                    context.SaveChanges();
                    var result = new { category.Id, category.Name, Status = "Changed" };
                    return Ok(result);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete_category")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                using var context = new StorageContext();
                var category = context.Categories.FirstOrDefault(x => x.Id == id);
                if (category is null)
                {
                    return StatusCode(404);
                }
                var result = new { category.Id, category.Name, Status = "Deleted" };
                context.Categories.Remove(category);
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
