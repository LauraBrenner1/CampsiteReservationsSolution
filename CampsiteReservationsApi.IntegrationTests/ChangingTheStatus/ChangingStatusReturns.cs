using Alba.Security;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CampsiteReservationsApi.IntegrationTests.ChangingTheStatus;

public class ChangingStatusReturns
{
    public class AcceptedIfGoodRequest: IClassFixture<BasicStatusContext>
    {
        private readonly IAlbaHost _host;
        private readonly BasicStatusContext _context;

        public AcceptedIfGoodRequest(BasicStatusContext context)
        {
            _host = context.AlbaHost;
            _context = context;
        }

        [Fact]
        public async Task OnPost()
        {
            var request = new PostStatusRequest("yummy");

           var response =  await _host.Scenario(api =>
            {
               
                api.WithClaim(new Claim("preferred_username", "Henry Gonzalez"));
                api.Post.Json(request).ToUrl("/status");
                api.StatusCodeShouldBe(HttpStatusCode.Accepted);
            });

            _context.MockedStatusService.Verify(v => v.UpdateStatusAsync("yummy", "some-subject", "Henry Gonzalez"), Times.Once);


        }
    }

    public class BadRequestIfRequestIsMalformed: IClassFixture<BasicStatusContext>
    {
        private readonly IAlbaHost _host;
        private readonly BasicStatusContext _context;

        public BadRequestIfRequestIsMalformed(BasicStatusContext context)
        {
            _host = context.AlbaHost;
            _context = context;
        }

        [Fact]
        public async Task OnPost()
        {
            var request = new PostStatusRequest("");

            var response = await _host.Scenario(api =>
            {
                api.Post.Json(request).ToUrl("/status");
                api.StatusCodeShouldBe(HttpStatusCode.BadRequest);
            });


            _context.MockedStatusService.Verify(m => m.UpdateStatusAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                ), Times.Never);
            
        }

        [Fact]
        public async Task OnPostWithDataButMissiungClaims()
        {
            var request = new PostStatusRequest("Some Data");

            var response = await _host.Scenario(api =>
            {
                api.IgnoreStatusCode();
                api.Post.Json(request).ToUrl("/status");
               // api.StatusCodeShouldBe(HttpStatusCode.BadRequest);
            });


            _context.MockedStatusService.Verify(m => m.UpdateStatusAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                ), Times.Never);

        }


    }

}


public class BasicStatusContext : WebAppFixture
{
    public Mock<IUpdateTheStatus>? MockedStatusService;
    public DateTime DateUsed = new DateTime(1969, 4, 20, 23, 59, 00);
    protected override void BuildServices(IServiceCollection builder)
    {
      var mockedUpdated = new Mock<IUpdateTheStatus>();

        var stubbedSystemTime = new Mock<ISystemTime>();
        stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(DateUsed);
        builder.AddScoped<IUpdateTheStatus>(_ => mockedUpdated.Object);
        builder.AddSingleton<ISystemTime>(_ => stubbedSystemTime.Object);
        MockedStatusService = mockedUpdated;
    }

    protected override JwtSecurityStub GetJwtSecurityStub()
    {
        return base.GetJwtSecurityStub().With("sub", "some-subject");
    }
}