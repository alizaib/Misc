using ApiApplication.Commands;
using ApiApplication.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IReservationService
    {
        Task<ReservationResponse> ReserveAsync(ReservationCommand command, CancellationToken cancellation);
    }
}
