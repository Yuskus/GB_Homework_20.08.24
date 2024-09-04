namespace HomeworkGB11.DatabaseModel.DTO
{
    public class PutEmployeeDTO
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Patronymic { get; set; }
        public required string Birthday { get; set; }
        public required string HiringDate { get; set; }
        public required string Adress { get; set; }
        public required string Phone { get; set; }
        public int WorkZoneId { get; set; }
        public int PositionId { get; set; }
    }
}
