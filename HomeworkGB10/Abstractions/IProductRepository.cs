using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Abstractions
{
    public interface IProductRepository : IRepository
    {
        public List<ProductDTO> GetProducts();
        public string GetProductsCsv();
        public int AddProduct(ProductDTO productDTO);
        public int PutProduct(ProductDTO productDTO);
        public int UpdatePriceOfProduct(int id, double price);
        public int DeleteProduct(int id);
    }
}
