using CampsiteReservationsApi.Models;
using System.Text.Json;

namespace CampsiteReservationsApi.Services;

public class OnCallApiService
{
    private readonly HttpClient _httpClient;

    public OnCallApiService(HttpClient httpClient)
    {
        _httpClient = httpClient; // It is against Federal Law* to ever call "new" on the HttpClient.
        // configure the client here...
    }

    public async Task<OnCappApiDeveloperInfo> GetOnCallDeveloperAsync()
    {
        var response = await _httpClient.GetAsync("/oncall");
        response.EnsureSuccessStatusCode(); // Throw an exception here if I didn't get a 200-299

        var content = await response.Content.ReadAsStringAsync();
        var info = JsonSerializer.Deserialize<OnCappApiDeveloperInfo>(content);

        return info!;
    }
}

