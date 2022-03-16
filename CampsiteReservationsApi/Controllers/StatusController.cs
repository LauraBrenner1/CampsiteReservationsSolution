using Microsoft.AspNetCore.Mvc;
using CampsiteReservationsApi.Models;
using CampsiteReservationsApi.Services;

namespace CampsiteReservationsApi.Controllers;

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

    [HttpPost("/status")]
    public async Task<ActionResult> UpdateTheStatus([FromBody] PostStatusRequest request)
    {
        // ?? What has to happen here?
        return Accepted();
    }
}


