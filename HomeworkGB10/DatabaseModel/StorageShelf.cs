namespace HomeworkGB10.DatabaseModel
{
    public class StorageShelf
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
