using CampsiteReservationsApi.Models;

namespace CampsiteReservationsApi.Services;

public class LocalStatusService : ICheckTheStatus
{
    private readonly ISystemTime _systemTime;

    public LocalStatusService(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    public Task<GetStatusResponse> GetCurrentStatusAsync()
    {
        return Task.FromResult(new GetStatusResponse("Looks Good", "Bob", _systemTime.GetCurrent()));
    }
}
