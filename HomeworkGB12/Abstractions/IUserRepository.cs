using HomeworkGB12.DatabaseModel.DTO;

namespace HomeworkGB12.Abstractions
{
    public interface IUserRepository
    {
        public void UserAdd(UserRightsDTO userRights);
        public UserRightsDTO? UserCheck(LoginFormDTO loginForm);
    }
}
