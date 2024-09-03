using AutoMapper;
using HomeworkGB10.DatabaseModel;
using HomeworkGB10.DatabaseModel.DTO;

namespace HomeworkGB10.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, GetCategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, GetProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<StorageShelf, GetStorageDTO>(MemberList.Destination).ReverseMap();

            CreateMap<Category, PutCategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, PutProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<StorageShelf, PutStorageDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
