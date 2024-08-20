namespace HomeworkGB10.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
