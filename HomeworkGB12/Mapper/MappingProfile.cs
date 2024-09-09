using AutoMapper;
using HomeworkGB12.DatabaseModel;
using HomeworkGB12.DatabaseModel.DTO;
using System.Text;

namespace HomeworkGB12.Mapper
{
    public class MappingProfile : Profile
    {
        MappingProfile()
        {
            CreateMap<UserEntity, PutUserRightsDTO>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => Encoding.UTF8.GetString(x.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role!.Name))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(x => x.RoleId));

            CreateMap<PutUserRightsDTO, UserEntity>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => Encoding.UTF8.GetBytes(x.Password)))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(x => x.Role));

            CreateMap<string, RoleEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x));

            CreateMap<RoleEntity, string>()
                .ForMember(dest => dest, opt => opt.MapFrom(x => x.Name));

            CreateMap<UserEntity, GetUserRightsDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role!.Name));
        }

        public static bool IsStringsEqual(string str1, string str2)
        {
            return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsUsersEqual(LoginFormDTO loginForm, UserEntity? userEntity)
        {
            return userEntity is not null 
                && userEntity.Password.Equals(loginForm.Password);
        }

        public static bool IsUsersEqual(PutUserRightsDTO userRights, UserEntity? userEntity)
        {
            return userEntity is not null 
                && userRights.Password.Equals(Encoding.UTF8.GetString(userEntity.Password))
                && userRights.Role.Equals(userEntity.Role!.Name);
        }


    }
}
