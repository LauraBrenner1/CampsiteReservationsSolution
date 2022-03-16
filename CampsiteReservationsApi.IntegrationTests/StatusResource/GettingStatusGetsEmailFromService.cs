global using Xunit;
global using Alba;
global using CampsiteReservationsApi.IntegrationTests.AlbaFixtures;
global using CampsiteReservationsApi.Models;
global using CampsiteReservationsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampsiteReservationsApi.IntegrationTests.StatusResource;

public class GettingStatusGetsEmailFromService : IClassFixture<GettingStatusGetEmailFromServiceContext>
{
    private readonly IAlbaHost _host;
    public GettingStatusGetsEmailFromService(GettingStatusGetEmailFromServiceContext context)
    {
        _host = context.AlbaHost;
    }

    [Fact]
    public async Task CanGetTheStatus()
    {
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });

        var content = response.ReadAsJson<GetStatusResponse>();
        Assert.Equal("henry@compuserve.com", content.oncall);

    }
}


public class GettingStatusGetEmailFromServiceContext : WebAppFixture
{
    protected override void BuildServices(IServiceCollection builder)
    {
        //var mockedEmailService = Mock.Of<ILookupOnCallDevelopers>(); // this is a dummy.
        var mockedEmailService = new Mock<ILookupOnCallDevelopers>();
        mockedEmailService.Setup(m => m.GetEmailAddressAsync()).ReturnsAsync("henry@compuserve.com");
        builder.AddTransient<ILookupOnCallDevelopers>((_) => mockedEmailService.Object);
       
    }
}