namespace HomeworkGB12.DatabaseModel.DTO
{
    public class PutUserRightsDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public int RoleId { get; set; }
    }
}
