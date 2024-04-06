using ApiApplication.Database.Entities;
using ApiApplication.Responses;
using AutoMapper;

namespace ApiApplication.MappingProfiles
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<SeatEntity, SeatResponse>();
        }
    }
}
