using CampsiteReservationsApi.IntegrationTests.AlbaFixtures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampsiteReservationsApi.IntegrationTests.StatusResource;

public class GetStatusMessageFromILookupApiStatus : IClassFixture<GetStatusFromILookupContextWithNoStoredStatus>
{

    private readonly IAlbaHost _host;

    public GetStatusMessageFromILookupApiStatus(GetStatusFromILookupContextWithNoStoredStatus context)
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

        Assert.Equal("There is no history of status for this API", content.status);
    }
}

public class GettingStatusFromWithStoredData : IClassFixture<GetStatusFromILookContextWithStoredStatus>
{

    private IAlbaHost _host;
    private GetStatusFromILookContextWithStoredStatus _context;

    public GettingStatusFromWithStoredData(GetStatusFromILookContextWithStoredStatus context)
    {
        _host = context.AlbaHost;
        _context = context;
    }

    [Fact]
    public async Task ReturnsTheStatus()
    {
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });

        var content = response.ReadAsJson<GetStatusResponse>();

        Assert.Equal(_context.ExpectedStatus, content.status);
    }
}

public class GetStatusFromILookupContextWithNoStoredStatus : WebAppFixture
{
    protected override void BuildServices(IServiceCollection builder)
    {
        var stubbedLookup = new Mock<ILookupApiStatus>();
        string cannedResponse = null;
        stubbedLookup.Setup(s => s.GetCurrentStatusAsync()).ReturnsAsync(cannedResponse);
        builder.AddScoped<ILookupApiStatus>((_) => stubbedLookup.Object);
    }
}




public class GetStatusFromILookContextWithStoredStatus : WebAppFixture
{

    public string ExpectedStatus = "Humming Along Niceley";
    protected override void BuildServices(IServiceCollection builder)
    {
        var stubbedLookup = new Mock<ILookupApiStatus>();
        
        stubbedLookup.Setup(s => s.GetCurrentStatusAsync()).ReturnsAsync(ExpectedStatus);
        builder.AddScoped<ILookupApiStatus>((_) => stubbedLookup.Object);
    }
}