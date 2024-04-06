using ApiApplication.Commands;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Repositories.Abstractions;
using ApiApplication.Responses;
using ApiApplication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IAuditoriumsRepository _auditoriumsRepo;
        private readonly IMapper _mapper;
        private readonly IShowtimesRepository _showtimesRepo;
        private readonly ITicketsRepository _ticketsRepo;
        private readonly ITicketExpiryPolicy _ticketExpiryPolicy;

        public TicketsController(IAuditoriumsRepository auditoriumsRepo, 
                                 IMapper mapper,
                                IShowtimesRepository showtimesRepo,
                                ITicketsRepository ticketsRepo,
                                ITicketExpiryPolicy ticketExpiryPolicy)
        {
            _auditoriumsRepo = auditoriumsRepo;
            _mapper = mapper;
            _showtimesRepo = showtimesRepo;
            _ticketsRepo = ticketsRepo;
            _ticketExpiryPolicy = ticketExpiryPolicy;
        }

        [HttpPost("reserve")]
        [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Reserve(ReservationCommand command, CancellationToken cancel)
        {
            var showTimeEntity = await _showtimesRepo.GetWithTicketsAndMoviesByIdAsync(command.ShowtimeId, cancel);
            var auditoriumEntity = await _auditoriumsRepo.GetAsync(showTimeEntity.AuditoriumId, cancel);
            var validTickets = showTimeEntity.Tickets.Where(t => !_ticketExpiryPolicy.IsExpired(t)).ToList();

            var (row, seat) = GetAvailableRowAndSeatNumber(auditoriumEntity.Seats, validTickets, command.NumberOfSeats);

            if (row == -1)
                return BadRequest($"{command.NumberOfSeats} consecutive seats are not available");

            var seats = new List<SeatEntity>();
            for(int i = seat; i < seat + command.NumberOfSeats; i++) {
                var seatEntity = auditoriumEntity.Seats.Single(s => s.Row == row && s.SeatNumber == i);                
                seats.Add(seatEntity);
            }
            
            var ticketEntity = await _ticketsRepo.CreateAsync(showTimeEntity, seats, cancel);
            await _auditoriumsRepo.SaveChanges(cancel);

            var rervationResponse = _mapper.Map<TicketEntity, ReservationResponse>(ticketEntity);

            return Ok(rervationResponse);            
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost("confirm/{id:guid}")]
        public async Task<IActionResult> Confirm([FromRoute]Guid id, CancellationToken cancel)
        {
            var ticketEntity = await _ticketsRepo.GetAsync(id, cancel);

            if (ticketEntity == null)
            {
                return NotFound($"No reservation found with Id : '{id}'");
            }
            if (ticketEntity.Paid)
            {
                return BadRequest($"Reservation with Id : '{id}' has already been paid for");
            }
            if (_ticketExpiryPolicy.IsExpired(ticketEntity))
            {   
                return BadRequest($"Cannot confirm reservation with Id : '{id}'. Reason: 'Expired'. Please make a new reservation");
            }            

            await _ticketsRepo.ConfirmPaymentAsync(ticketEntity, cancel);

            return Ok($"Your reservation with Id: '{id}' is confirmed. Thank you");
        }

        private (int row, int seat) GetAvailableRowAndSeatNumber(ICollection<SeatEntity> seats,
                                                                 ICollection<TicketEntity> ticketEntities,
                                                                 int numberOfSeats) {

            var rows = seats.GroupBy(seat => seat.Row).OrderBy(rows => rows.Key);
            int curSeatNumber, availableSeats, totalSeats;

            foreach (var row in rows)
            {
                curSeatNumber = 1; //Reset variables
                availableSeats = 0; 
                totalSeats = row.Count();

                foreach (var seat in row)
                {
                    if (ticketEntities.Any(t => t.Seats.Contains(seat)))
                    {
                        int remainingSeatsInRow = totalSeats - curSeatNumber;
                        if (remainingSeatsInRow < numberOfSeats) break;  // No more enough seats left to check
                        else {
                            availableSeats = 0;
                            curSeatNumber++;
                            continue;
                        }
                    }
                    else
                    {
                        availableSeats++;
                        if (availableSeats == numberOfSeats)
                        {
                            var firstSeat = curSeatNumber - availableSeats + 1;
                            return (row.Key, firstSeat);
                        }
                        curSeatNumber++;
                    }
                }
            }

            return (-1, -1);
        }
    }
}
