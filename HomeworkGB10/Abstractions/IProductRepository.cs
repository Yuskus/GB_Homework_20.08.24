using HomeworkGB10.DatabaseModel.DTO;

namespace HomeworkGB10.Abstractions
{
    public interface IProductRepository : IRepository
    {
        public List<GetProductDTO> GetProducts();
        public string GetProductsCsv();
        public string GetProductsCsvUrl();
        public int AddProduct(PutProductDTO productDTO);
        public int PutProduct(PutProductDTO productDTO);
        public int UpdatePriceOfProduct(int id, double price);
        public int DeleteProduct(int id);
    }
}
