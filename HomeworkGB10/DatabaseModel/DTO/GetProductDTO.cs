namespace HomeworkGB10.Models.DTO
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
