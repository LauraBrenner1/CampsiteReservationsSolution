using Microsoft.AspNetCore.Mvc;
using CampsiteReservationsApi.Models;
using CampsiteReservationsApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CampsiteReservationsApi.Controllers;

[ApiController]
public class StatusController : ControllerBase
{
    private readonly ICheckTheStatus _statusChecker;
    private readonly IUpdateTheStatus _statusUpdater;

    public StatusController(ICheckTheStatus statusChecker, IUpdateTheStatus statusUpdater)
    {
        _statusChecker = statusChecker;
        _statusUpdater = statusUpdater;
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

        if (sub is null || userName is null)
        {
            return StatusCode(403);
        }
        else
        {
            await _statusUpdater.UpdateStatusAsync(request.status, sub, userName);
            return Accepted();
        }
    }
}


