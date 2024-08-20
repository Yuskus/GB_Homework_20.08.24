using HomeworkGB10.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet(template: "get_products")]
        public ActionResult GetProducts()
        {
            try
            {
                using var context = new StorageContext();
                var products = context.Products.Select(x => new { x.Id, x.Name, x.Description }).ToList();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost(template: "post_product")]
        public ActionResult AddProduct(string name, string description, double price, int categoryId)
        {
            try
            {
                using var context = new StorageContext();
                if (!context.Products.Any(x => x.Name == name))
                {
                    var product = new Product()
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        CategoryId = categoryId
                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                    var result = new { product.Id, description, price, categoryId, Status = "Added" };
                    return Ok(result);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "put_product")]
        public ActionResult PutProduct(int id, string name, string description, double price, int categoryId)
        {
            try
            {
                using var context = new StorageContext();
                var product = context.Products.FirstOrDefault(x => x.Id == id);
                if (product is null)
                {
                    var createProduct = new Product()
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        CategoryId = categoryId
                    };
                    context.Products.Add(createProduct);
                    context.SaveChanges();
                    var result = new { createProduct.Id, name, description, price, categoryId, Status = "Added" };
                    return Ok(result);
                }
                else
                {
                    product.Name = name;
                    product.Description = description;
                    product.Price = price;
                    product.CategoryId = categoryId;
                    context.SaveChanges();
                    var result = new { product.Id, name, description, price, categoryId, Status = "Changed" };
                    return Ok(result);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "patch_product")]
        public ActionResult UpdatePriceOfProduct(string name, double price)
        {
            try
            {
                using var context = new StorageContext();
                var product = context.Products.FirstOrDefault(x => x.Name == name);
                if (product is null) { return StatusCode(404); }
                product.Price = price;
                context.SaveChanges();
                var result = new { product.Id, name, price, product.CategoryId, Status = "Changed" };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "delete_product")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                using var context = new StorageContext();
                var product = context.Products.FirstOrDefault(x => x.Id == id);
                if (product is null)
                {
                    return StatusCode(404);
                }
                var result = new { product.Id, product.Name, product.Price, Status = "Deleted" };
                context.Products.Remove(product);
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
