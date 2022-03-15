using CampsiteReservationsApi.Models;

namespace CampsiteReservationsApi.Services;

public interface ICheckTheStatus
{
    Task<GetStatusResponse> GetCurrentStatusAsync();
}
