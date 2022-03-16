using CampsiteReservationsApi.IntegrationTests.AlbaFixtures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampsiteReservationsApi.IntegrationTests.StatusResource;

public class GetStatusMessageFromILookupApiStatus : IClassFixture<GetStatusFromILookupContext>
{

    private readonly IAlbaHost _host;

    public GetStatusMessageFromILookupApiStatus(GetStatusFromILookupContext context)
    {
        _host = context.AlbaHost;
    }

    [Fact]
    public async Task ShowsRightStatus()
    {
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();

        });

        var content = response.ReadAsJson<GetStatusResponse>();

        Assert.Equal("tacos", content.status);
    }
}


public class GetStatusFromILookupContext : WebAppFixture
{
    protected override void BuildServices(IServiceCollection builder)
    {
        var stubbedLookup = new Mock<ILookupApiStatus>();
        stubbedLookup.Setup(s => s.GetCurrentStatusAsync()).ReturnsAsync("tacos");
        builder.AddScoped<ILookupApiStatus>((_) => stubbedLookup.Object);
    }
}