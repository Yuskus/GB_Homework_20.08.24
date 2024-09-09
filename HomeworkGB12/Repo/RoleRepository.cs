using AutoMapper;
using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel;
using System.Security.Claims;

namespace HomeworkGB12.Repo
{
    public class RoleRepository(IAuthenticateDbContext context, IMapper mapper) : IRoleRepository
    {
        private readonly IAuthenticateDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public int AddRole(string newRole)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name.Equals(newRole));
            if (role is null)
            {
                role = _mapper.Map<RoleEntity>(newRole);
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            return role.Id;
        }

        public string? CheckRole(HttpContext httpContext)
        {
            if (httpContext.User.Identity is not ClaimsIdentity identity) return null;

            return identity.Claims.LastOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }
    }
}
