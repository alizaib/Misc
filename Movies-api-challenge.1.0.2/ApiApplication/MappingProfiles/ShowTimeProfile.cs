using ApiApplication.Database.Entities;
using ApiApplication.Responses;
using AutoMapper;

namespace ApiApplication.MappingProfiles
{
    public class ShowTimeProfile : Profile
    {
        public ShowTimeProfile()
        {
            CreateMap<MovieEntity, MovieResponse>();
            CreateMap<ShowtimeEntity, ShowtimeResponse>();                
        }
    }
}
