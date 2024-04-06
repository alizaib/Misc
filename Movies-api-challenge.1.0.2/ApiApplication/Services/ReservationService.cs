using ApiApplication.Commands;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Repositories.Abstractions;
using ApiApplication.Models;
using ApiApplication.Responses;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IAuditoriumsRepository _auditoriumsRepo;
        private readonly IMapper _mapper;
        private readonly IShowtimesRepository _showtimesRepo;
        private readonly ITicketsRepository _ticketsRepo;
        private readonly ITicketExpiryPolicy _ticketExpiryPolicy;
        private readonly IReservationHelper _reservationHelper;

        public ReservationService(IAuditoriumsRepository auditoriumsRepo,
                                 IMapper mapper,
                                 IShowtimesRepository showtimesRepo,
                                 ITicketsRepository ticketsRepo,
                                 ITicketExpiryPolicy ticketExpiryPolicy,
                                 IReservationHelper reservationHelper)
        {
            _auditoriumsRepo = auditoriumsRepo;
            _mapper = mapper;
            _showtimesRepo = showtimesRepo;
            _ticketsRepo = ticketsRepo;
            _ticketExpiryPolicy = ticketExpiryPolicy;
            _reservationHelper = reservationHelper;
        }
        public async Task<ReservationResponse> ReserveAsync(ReservationCommand command, CancellationToken cancel)
        {
            var showTimeEntity = await _showtimesRepo.GetWithTicketsAndMoviesByIdAsync(command.ShowtimeId, cancel);
            var auditoriumEntity = await _auditoriumsRepo.GetAsync(showTimeEntity.AuditoriumId, cancel);
            var validTickets = showTimeEntity.Tickets.Where(t => !_ticketExpiryPolicy.IsExpired(t)).ToList();

            var seat = _reservationHelper.FindFirstAvailableNSeats(auditoriumEntity.Seats.ToList(), validTickets, command.NumberOfSeats);

            if (seat == Seat.None)
                return null;

            var seats = new List<SeatEntity>();
            for (int i = seat.SeatNumber; i < seat.SeatNumber + command.NumberOfSeats; i++)
            {
                var seatEntity = auditoriumEntity.Seats.Single(s => s.Row == seat.RowNumber && s.SeatNumber == i);
                seats.Add(seatEntity);
            }

            var ticketEntity = await _ticketsRepo.CreateAsync(showTimeEntity, seats, cancel);
            await _auditoriumsRepo.SaveChanges(cancel);

            var rervationResponse = _mapper.Map<TicketEntity, ReservationResponse>(ticketEntity);

            return rervationResponse;
        }
    }
}

    
