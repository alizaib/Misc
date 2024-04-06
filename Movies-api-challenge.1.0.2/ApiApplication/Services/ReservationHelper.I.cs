using ApiApplication.Database.Entities;
using ApiApplication.Models;
using System.Collections.Generic;

namespace ApiApplication.Services
{
    public interface IReservationHelper
    {
        Seat FindFirstAvailableNSeats(IReadOnlyCollection<SeatEntity> seats,
                                     IReadOnlyCollection<TicketEntity> ticketEntities,
                                     int numberOfSeats);
    }
}
