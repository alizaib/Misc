using System;

namespace ApiApplication.Commands
{
    public class CreateShowTimeCommand
    {
        public int AuditoriumId { get; set; }
        public string ImdbId { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
