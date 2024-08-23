using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Abstractions
{
    public interface ICategoryRepository : IRepository
    {
        public List<CategoryDTO> GetCategories();
        public string GetCategoriesCsv();
        public int AddCategory(CategoryDTO category);
        public int PutCategory(CategoryDTO categoryDTO);
        public int DeleteCategory(int id);
    }
}
