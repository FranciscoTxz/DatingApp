using System.Reflection.Metadata;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, MemberResponse>();
        CreateMap<Photo, PhotoResponse>();
    }
}