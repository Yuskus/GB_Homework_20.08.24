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
            /*CreateMap<LoginFormDTO, UserEntity>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => Encoding.UTF8.GetBytes(x.Password)))
                .ForMember(dest => dest.Salt, opt => );*/

            CreateMap<UserEntity, UserRightsDTO>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => Encoding.UTF8.GetString(x.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role!.Id));

            CreateMap<UserRightsDTO, UserEntity>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => Encoding.UTF8.GetBytes(x.Password)))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(x => x.Role));
        }
    }
}
