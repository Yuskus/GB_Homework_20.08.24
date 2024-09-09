using HomeworkGB12.DatabaseModel.DTO;

namespace HomeworkGB12.Abstractions
{
    public interface IUserRepository
    {
        public int AddUser(PutUserRightsDTO userRights);
    }
}
