namespace HomeworkGB11.DatabaseModel.DTO
{
    public class PutPositionDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal BaseSalary { get; set; }
    }
}
