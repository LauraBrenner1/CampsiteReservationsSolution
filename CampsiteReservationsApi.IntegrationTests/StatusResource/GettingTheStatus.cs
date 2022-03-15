using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using System;

namespace CampsiteReservationsApi.IntegrationTests.StatusResource;

public class GettingTheStatus : IClassFixture<CustomBlankWebApplicationFactory<Program>>
{

    private readonly HttpClient _client;

    public GettingTheStatus(CustomBlankWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsA200()
    {

        var response = await _client.GetAsync("/status");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ReturnsJsonData()
    {
        var response = await _client.GetAsync("/status");

        var contentType = response.Content?.Headers?.ContentType?.MediaType;

        Assert.Equal("application/json", contentType);
    }

    [Fact]
    public async Task ReturnsTheStatusInformation()
    {
        var response = await _client.GetAsync("/status");

        var content = await response.Content.ReadAsStringAsync();

        var entity = JsonSerializer.Deserialize<StatusResponse>(content);

        Assert.Equal("Looks Good", entity?.status);
        Assert.Equal("Bob", entity?.oncall);

    }
}


public class StatusResponse
{
    public string status { get; set; }
    public string oncall { get; set; }
    public DateTime whenChecked { get; set; }
}
