using System;
using System.Collections.Generic;

namespace ApiApplication.Responses
{
    public class AuditoriumResponse
    {
        public int Id { get; set; }
        public IEnumerable<ShowtimeResponse> ShowTimes { get; set; }
        public IEnumerable<SeatResponse> Seats { get; set; }
    }
}
