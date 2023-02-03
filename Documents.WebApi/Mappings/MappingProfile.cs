using AutoMapper;
using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Entities.EntitiesLocationData;
using Documents.WebApi.Models.DTOs;

namespace Profiles.WebApi.Mappings; 
public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<Blob, BlobDto>().ReverseMap();
        CreateMap<BlobResponse, BlobResponseDto>().ReverseMap();
    }
}
