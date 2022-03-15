using Alba;
using CampsiteReservationsApi.IntegrationTests.AlbaFixtures;
using CampsiteReservationsApi.Models;
using CampsiteReservationsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CampsiteReservationsApi.IntegrationTests.StatusResource;

public class GettingTheStataAlbaStyle : IClassFixture<StubbedFixtureWithStatus> 
{
    private readonly IAlbaHost _host;

    public GettingTheStataAlbaStyle(StubbedFixtureWithStatus app)
    {
        _host = app.AlbaHost;
    }

    [Fact]
    public async Task CanGetStatus()
    {
       var result = await _host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });

        var fromApi = result.ReadAsJson<GetStatusResponse>();

        Assert.Equal(TestObjects.GetStubbedStatusResponse(), fromApi);
    }
}


public class StubbedFixtureWithStatus : WebAppFixture
{
    protected override void BuildServices(IServiceCollection builder)
    {
        var stubbedService = new Mock<ICheckTheStatus>();
        stubbedService.Setup(s => s.GetCurrentStatusAsync()).ReturnsAsync(TestObjects.GetStubbedStatusResponse());

        builder.AddTransient<ICheckTheStatus>((_) => stubbedService.Object);

    }
}