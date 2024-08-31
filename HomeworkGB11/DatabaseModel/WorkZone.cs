namespace HomeworkGB11.DatabaseModel
{
    public class WorkZone
    {
        public int Id { get; set; }
        public required string ZoneName { get; set; }
        public virtual ICollection<EmployeeInfo> Employees { get; set; } = [];
    }
}
