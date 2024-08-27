using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Abstractions
{
    public interface ICategoryRepository : IRepository
    {
        public List<GetCategoryDTO> GetCategories();
        public string GetCategoriesCsv();
        public string GetCategoriesCsvUrl();
        public int AddCategory(PutCategoryDTO category);
        public int UpdateCategory(int id, string name);
        public int DeleteCategory(int id);
    }
}
