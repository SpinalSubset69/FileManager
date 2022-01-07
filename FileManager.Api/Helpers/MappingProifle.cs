using AutoMapper;
using FileManager.Entities.Dtos.UserDtos;
using FileManager.Entities.Entities;

namespace FileManager.Api.Helpers;

public class MappingProifle : Profile
{
    public MappingProifle()
    {
        //Register Mappers
        CreateMap<User, RegisterUserDto>().ReverseMap();
        CreateMap<User, UpdateUserInfoDto>().ReverseMap();
        CreateMap<Folder, RegisterFolderDto>().ReverseMap();  

        //User Profiles
        CreateMap<User, UserToReturnDto>().ReverseMap();    
    }
}
