namespace HomeworkGB10.DatabaseModel.DTO
{
    public class PutProductDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
