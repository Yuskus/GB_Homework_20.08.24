using AutoMapper;
using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel.DTO;
using HomeworkGB12.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeworkGB12.Repo
{
    public class LoginRepository(IAuthenticateDbContext context, IConfiguration configuration, IMapper mapper) : ILoginRepository
    {
        private readonly IAuthenticateDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;

        public string Authenticate(LoginFormDTO loginForm)
        {
            var user = _context.Users.FirstOrDefault(x => MappingProfile.IsStringsEqual(x.Username, loginForm.Username));

            if (MappingProfile.IsUsersEqual(loginForm, user))
            {
                var userRights = _mapper.Map<PutUserRightsDTO>(user);
                return GenerateToken(userRights);
            }

            return "Неверный логин или пароль.";
        }

        private string GenerateToken(PutUserRightsDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                             _configuration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(60),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
