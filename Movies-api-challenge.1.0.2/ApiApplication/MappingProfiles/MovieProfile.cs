using ApiApplication.Database.Entities;
using AutoMapper;
using ProtoDefinitions;
using System;

namespace ApiApplication.MappingProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<showResponse, MovieEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.FullTitle))
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Crew))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => new DateTime(int.Parse(src.Year), 1, 1)));
        }
    }
}
