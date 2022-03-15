namespace CampsiteReservationsApi.Services;

public class RemoteOnCallDeveloperLookup : ILookupOnCallDevelopers
{
    private readonly OnCallApiService _onCallApiService;

    public RemoteOnCallDeveloperLookup(OnCallApiService onCallApiService)
    {
        _onCallApiService = onCallApiService;
    }

    public async Task<string> GetEmailAddressAsync()
    {
        try
        {
            var response = await _onCallApiService.GetOnCallDeveloperAsync();

            return response.emailAddress;
        }
        catch (HttpRequestException)
        {

            throw new UnableToProvideOnCallDeveloperException();
        }
    }
}
