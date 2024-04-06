using System;

namespace ApiApplication.Responses
{
    public class ShowtimeResponse
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public MovieResponse Movie { get; set; }
        public int AuditoriumId { get; set; }
    }
}
