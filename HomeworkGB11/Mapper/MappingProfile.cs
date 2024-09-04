using AutoMapper;
using HomeworkGB11.DatabaseModel;
using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeInfo, GetEmployeeDTO>().ReverseMap();
            CreateMap<EmployeePosition, GetPositionDTO>().ReverseMap();
            CreateMap<WorkZone, GetWorkZoneDTO>().ReverseMap();

            CreateMap<EmployeeInfo, PutEmployeeDTO>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(s => s.Birthday.ToString()))
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(s => s.HiringDate.ToString()));

            CreateMap<PutEmployeeDTO, EmployeeInfo>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(s => DateOnly.Parse(s.Birthday)))
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(s => DateOnly.Parse(s.HiringDate)));

            CreateMap<EmployeePosition, PutPositionDTO>().ReverseMap();
            CreateMap<WorkZone, PutWorkZoneDTO>().ReverseMap();
        }
    }
}
