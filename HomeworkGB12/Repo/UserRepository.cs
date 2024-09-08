using AutoMapper;
using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel;
using HomeworkGB12.DatabaseModel.DTO;
using System.Text;

namespace HomeworkGB12.Repo
{
    public class UserRepository(IAuthenticateDbContext context, IMapper mapper) : IUserRepository
    {
        private readonly IAuthenticateDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public void UserAdd(UserRightsDTO userRights)
        {
            if (!_context.Users.Any(x =>
                    x.Username.Equals(userRights.Username) &&
                    x.Password.Equals(userRights.Password)))
            {
                var entity = _mapper.Map<UserEntity>(userRights);
                _context.Users.Add(entity);
                _context.SaveChanges();
            }
        }

        public UserRightsDTO? UserCheck(LoginFormDTO loginForm)
        {
            var user = _context.Users.FirstOrDefault(x => 
                    x.Username.Equals(loginForm.Username) && 
                    x.Password.Equals(loginForm.Password));
            return user is null ? null : _mapper.Map<UserRightsDTO>(user);
        }
    }
}
