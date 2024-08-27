using AutoMapper;
using HomeworkGB10.Models;
using HomeworkGB10.Models.DTO;

namespace HomeworkGB10.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, GetCategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, GetProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, GetStorageDTO>(MemberList.Destination).ReverseMap();

            CreateMap<Category, PutCategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, PutProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, PutStorageDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
