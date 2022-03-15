using CampsiteReservationsApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CampsiteReservationsApi.IntegrationTests;

public class CustomBlankWebApplicationFactory<T>: WebApplicationFactory<T> where T: class 
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var stubbedStatusService = new Mock<ICheckTheStatus>();
            stubbedStatusService.Setup(s => s.GetCurrentStatusAsync()).ReturnsAsync(TestObjects.GetStubbedStatusResponse());
            services.AddTransient<ICheckTheStatus>((_) => stubbedStatusService.Object);
        });
    }
}
