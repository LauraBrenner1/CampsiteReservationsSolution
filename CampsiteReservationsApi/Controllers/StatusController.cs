using Microsoft.AspNetCore.Mvc;
using CampsiteReservationsApi.Models;
using CampsiteReservationsApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CampsiteReservationsApi.Controllers;

[ApiController]
public class StatusController : ControllerBase
{
    private readonly ICheckTheStatus _statusChecker;

    public StatusController(ICheckTheStatus statusChecker)
    {
        _statusChecker = statusChecker;
    }

    [HttpGet("/status")]
    public async Task<IActionResult> GetTheStatus()
    {

        GetStatusResponse response = await _statusChecker.GetCurrentStatusAsync();


        return Ok(response);
    }

    [Authorize]
    [HttpPost("/status")]
    public async Task<ActionResult> UpdateTheStatus([FromBody] PostStatusRequest request)
    {

        var sub = User?.GetSub();
        var userName = User?.GetPreferredUserName();
        
        // ?? What has to happen here?
        return Accepted();
    }
}


