using Alba;
using CampsiteReservationsApi.IntegrationTests.AlbaFixtures;
using CampsiteReservationsApi.Models;
using CampsiteReservationsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CampsiteReservationsApi.IntegrationTests.StatusResource;

public class GetStatusWhenRemoteServiceIsUnavilable : IClassFixture<StubbedFixtureWithFailingService>
{
    private readonly IAlbaHost _host;

    public GetStatusWhenRemoteServiceIsUnavilable(StubbedFixtureWithFailingService app)
    {
        _host = app.AlbaHost;
    }

    [Fact]
    public async Task ProvidesDefaultResponseForDeveloper()
    {
        var result = await _host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });

        var fromApi = result.ReadAsJson<GetStatusResponse>();


        //var expectedResponse = TestObjects.GetStubbedStatusResponse() with { oncall = "laura@aol.com" };

        Assert.Equal("laura@aol.com", fromApi.oncall);
       
    }
}


public class StubbedFixtureWithFailingService : WebAppFixture
{
    protected override void BuildServices(IServiceCollection builder)
    {
        var stubbedService = new Mock<ILookupOnCallDevelopers>();
        stubbedService.Setup(s => s.GetEmailAddressAsync()).ThrowsAsync(new UnableToProvideOnCallDeveloperException());

        builder.AddTransient<ILookupOnCallDevelopers>((_) => stubbedService.Object);

    }
}

/* Do the same thing as the GetingTheStatus, but the service that we use returns a different result because of an error */

