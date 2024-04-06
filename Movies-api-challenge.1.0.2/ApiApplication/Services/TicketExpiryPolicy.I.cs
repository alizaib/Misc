using ApiApplication.Database.Entities;

namespace ApiApplication.Services
{
    public interface ITicketExpiryPolicy
    {
        bool IsExpired(TicketEntity ticketEntity);
    }
}
