namespace HomeworkGB11.DatabaseModel.DTO
{
    public class GetEmployeeDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateOnly Birthday { get; set; }
        public DateOnly HiringDate { get; set; }
        public required string Adress { get; set; }
        public required string Phone { get; set; }
        public int WorkZoneId { get; set; }
        public int PositionId { get; set; }
    }
}
