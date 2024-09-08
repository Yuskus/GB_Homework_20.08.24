namespace HomeworkGB12.DatabaseModel.DTO
{
    public class UserRightsDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int Role { get; set; }
    }
}
