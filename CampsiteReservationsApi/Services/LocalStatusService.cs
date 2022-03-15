using CampsiteReservationsApi.Models;

namespace CampsiteReservationsApi.Services;

public class LocalStatusService : ICheckTheStatus
{
    private readonly ISystemTime _systemTime;
    private readonly ILookupOnCallDevelopers _onCallDeveloperLookup;

    public LocalStatusService(ISystemTime systemTime, ILookupOnCallDevelopers onCallDeveloperLookup)
    {
        _systemTime = systemTime;
        _onCallDeveloperLookup = onCallDeveloperLookup;
    }

    public async Task<GetStatusResponse> GetCurrentStatusAsync()
    {
        string emailAddressOfOnCallPerson = await _onCallDeveloperLookup.GetEmailAddressAsync();
        return new GetStatusResponse("Looks Good", emailAddressOfOnCallPerson, _systemTime.GetCurrent());
    }
}
