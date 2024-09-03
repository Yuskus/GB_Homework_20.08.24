namespace HomeworkGB10.DatabaseModel
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required double Price { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<StorageShelf> Storages { get; set; } = [];
    }
}
