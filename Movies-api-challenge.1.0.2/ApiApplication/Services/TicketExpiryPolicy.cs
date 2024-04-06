using ApiApplication.Database.Entities;
using Microsoft.AspNetCore.Authentication;
namespace ApiApplication.Services
{
    public class TicketExpiryPolicy : ITicketExpiryPolicy
    {
        private const int ExpireAfterMinutes = 10;
        private readonly ISystemClock _clock;

        public TicketExpiryPolicy(ISystemClock clock)
        {
            _clock = clock;
        }

        public bool IsExpired(TicketEntity ticketEntity)
        {
            var now = _clock.UtcNow.LocalDateTime;

            return !ticketEntity.Paid
                && ticketEntity.CreatedTime.AddMinutes(ExpireAfterMinutes) < now;
        }
    }
}
