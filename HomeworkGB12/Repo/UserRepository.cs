using AutoMapper;
using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel;
using HomeworkGB12.DatabaseModel.DTO;
using HomeworkGB12.Mapper;

namespace HomeworkGB12.Repo
{
    public class UserRepository(IAuthenticateDbContext context, IMapper mapper) : IUserRepository
    {
        private readonly IAuthenticateDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public int AddUser(PutUserRightsDTO userRights)
        {
            var user = _context.Users.FirstOrDefault(x => MappingProfile.IsStringsEqual(x.Username, userRights.Username));

            if (user is null)
            {
                var entity = _mapper.Map<UserEntity>(userRights);
                _context.Users.Add(entity);
                _context.SaveChanges();
            }

            return user!.Id;
        }
    }
}
