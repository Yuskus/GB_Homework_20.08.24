namespace HomeworkGB12.DatabaseModel
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required byte[] Password { get; set; }
        public required byte[] Salt { get; set; }
        public int RoleId { get; set; }
        public virtual RoleEntity? Role { get; set; }
    }
}
