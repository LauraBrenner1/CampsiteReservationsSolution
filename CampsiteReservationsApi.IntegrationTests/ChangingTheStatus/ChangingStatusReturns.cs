using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CampsiteReservationsApi.IntegrationTests.ChangingTheStatus;

public class ChangingStatusReturns
{
    public class AcceptedIfGoodRequest: IClassFixture<BasicStatusContext>
    {
        private readonly IAlbaHost _host;

        public AcceptedIfGoodRequest(BasicStatusContext context)
        {
            _host = context.AlbaHost;
        }

        [Fact]
        public async Task OnPost()
        {
            var request = new PostStatusRequest("yummy");

            await _host.Scenario(api =>
            {
                api.Post.Json(request).ToUrl("/status");
                api.StatusCodeShouldBe(HttpStatusCode.Accepted);
            });
        }
    }

    public class BadRequestIfRequestIsMalformed: IClassFixture<BasicStatusContext>
    {
        private readonly IAlbaHost _host;

        public BadRequestIfRequestIsMalformed(BasicStatusContext context)
        {
            _host = context.AlbaHost;
        }

        [Fact]
        public async Task OnPost()
        {
            var request = new PostStatusRequest("");

            await _host.Scenario(api =>
            {
                api.Post.Json(request).ToUrl("/status");
                api.StatusCodeShouldBe(HttpStatusCode.BadRequest);
            });
        }
    }

}


public class BasicStatusContext : WebAppFixture
{
    
    protected override void BuildServices(IServiceCollection builder)
    {
        // nothing yet.
    }
}