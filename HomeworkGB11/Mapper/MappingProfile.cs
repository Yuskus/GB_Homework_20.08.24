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

            CreateMap<EmployeeInfo, PutEmployeeDTO>().ReverseMap();
            CreateMap<EmployeePosition, PutPositionDTO>().ReverseMap();
            CreateMap<WorkZone, PutWorkZoneDTO>().ReverseMap();
        }
    }
}
