using AutoMapper;
using HomeworkGB10.Models;
using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
