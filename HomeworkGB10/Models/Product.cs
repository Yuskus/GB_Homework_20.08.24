namespace HomeworkGB10.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Storage> Storages { get; set; } = [];
    }
}
