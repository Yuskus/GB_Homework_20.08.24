namespace HomeworkGB12.DatabaseModel.DTO
{
    public class GetUserRightsDTO
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
        public int RoleId { get; set; }
    }
}
