using ApiApplication.Database.Entities;
using ApiApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Services
{
    public class ReservationHelper : IReservationHelper
    {

        public Seat FindFirstAvailableNSeats(IReadOnlyCollection<SeatEntity> seats,
                                           IReadOnlyCollection<TicketEntity> ticketEntities,
                                           int numberOfSeats)
        {

            var rows = seats.GroupBy(seat => seat.Row).OrderBy(rows => rows.Key);

            foreach (var row in rows)
            {
                var rowNav = new RowNavigator(row.Count());

                foreach (var seat in row)
                {
                    if (ticketEntities.Any(t => t.Seats.Contains(seat)))
                    {
                        if (rowNav.RemaingSeatsInRow < numberOfSeats) break;  // No more enough seats left to check
                        else
                        {
                            rowNav.Move();
                            continue;
                        }
                    }
                    else
                    {
                        rowNav.MarkAndMove();
                        if (rowNav.AvailableSeats == numberOfSeats)
                        {
                            var firstSeat = rowNav.GetFirstAvailableSeat();
                            return new Seat(row.Key, firstSeat);
                        }
                    }
                }
            }

            return Seat.None;
        }
    }
}
