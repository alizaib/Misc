using ApiApplication.Database.Entities;
using ApiApplication.Responses;
using AutoMapper;

namespace ApiApplication.MappingProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketEntity, ReservationResponse>()
                .ForMember(dest => dest.AuditoriumId, opt => opt.MapFrom(src => src.Showtime.AuditoriumId))
                .ForMember(dest => dest.SessionDate, opt => opt.MapFrom(src => src.Showtime.SessionDate))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Showtime.Movie.Title));
        }
    }
}
