using Alba;
using Alba.Security;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace CampsiteReservationsApi.IntegrationTests.AlbaFixtures;

public abstract class WebAppFixture : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null;


    public async Task DisposeAsync()
    {
        await AlbaHost.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        AlbaHost = await Alba.AlbaHost.For<Program>(builder =>
        {

            builder.ConfigureServices((context, services) =>
            {
                BuildServices(services);
            });
        }, GetJwtSecurityStub());
    }

    protected virtual void BuildServices(IServiceCollection builder)
    {

    }

    protected virtual JwtSecurityStub GetJwtSecurityStub()
    {
        return new JwtSecurityStub();
    }

}
