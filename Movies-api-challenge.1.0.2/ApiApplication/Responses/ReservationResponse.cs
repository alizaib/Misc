using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Responses
{
    public class ReservationResponse
    {
        //GUID of the reservation,  and the movie that will be played.
        public Guid Id { get; set; }

        //the number of seats
        public IEnumerable<SeatResponse> Seats { get; set; }
        public int TotalSeats => Seats.Count();

        //the auditorium used
        public int AuditoriumId { get; set; }

        //and the movie that will be played.
        public string MovieTitle { get; set; }

        public DateTime SessionDate { get; set; }
    }
}
