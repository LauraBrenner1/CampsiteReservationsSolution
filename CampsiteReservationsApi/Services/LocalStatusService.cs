using CampsiteReservationsApi.Models;

namespace CampsiteReservationsApi.Services;

public class LocalStatusService : ICheckTheStatus
{
    private readonly ISystemTime _systemTime;
    private readonly ILookupOnCallDevelopers _onCallDeveloperLookup;
    private readonly ILookupApiStatus _apiStatusLookup;
    public LocalStatusService(ISystemTime systemTime, ILookupOnCallDevelopers onCallDeveloperLookup, ILookupApiStatus apiStatusLookup)
    {
        _systemTime = systemTime;
        _onCallDeveloperLookup = onCallDeveloperLookup;
        _apiStatusLookup = apiStatusLookup;
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

        string status = await _apiStatusLookup.GetCurrentStatusAsync();

        return new GetStatusResponse(status, emailAddress, _systemTime.GetCurrent());
    }
}
