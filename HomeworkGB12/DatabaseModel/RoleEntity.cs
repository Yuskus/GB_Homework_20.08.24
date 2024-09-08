namespace HomeworkGB12.DatabaseModel
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; } = [];
    }
}
