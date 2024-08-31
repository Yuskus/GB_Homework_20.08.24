namespace HomeworkGB11.DatabaseModel
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal BaseSalary { get; set; }
        public virtual ICollection<EmployeeInfo> Employees { get; set; } = [];
    }
}
