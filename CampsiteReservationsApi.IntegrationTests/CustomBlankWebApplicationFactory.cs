using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CampsiteReservationsApi.IntegrationTests;

public class CustomBlankWebApplicationFactory<T>: WebApplicationFactory<T> where T: class 
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

    }
}
