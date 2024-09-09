using HomeworkGB12.DatabaseModel.DTO;

namespace HomeworkGB12.Abstractions
{
    public interface ILoginRepository
    {
        public string? Authenticate(LoginFormDTO loginForm);
    }
}
