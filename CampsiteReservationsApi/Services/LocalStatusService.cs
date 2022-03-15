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
        var emailAddress = "";
        try
        {
             emailAddress = await _onCallDeveloperLookup.GetEmailAddressAsync();
        }
        catch (UnableToProvideOnCallDeveloperException)
        {

            emailAddress = "laura@aol.com";
        }
        return new GetStatusResponse("Looks Good", emailAddress, _systemTime.GetCurrent());
    }
}
