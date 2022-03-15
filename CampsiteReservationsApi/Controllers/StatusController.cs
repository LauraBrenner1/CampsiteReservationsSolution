using Microsoft.AspNetCore.Mvc;

namespace CampsiteReservationsApi.Controllers;

public class StatusController : ControllerBase
{
    [HttpGet("/status")]
    public async Task<IActionResult> GetTheStatus()
    {
        return Ok(new GetStatusResponse("Looks Good", "Bob", DateTime.Now));
    }
}

public record GetStatusResponse(string status, string oncall, DateTime whenChecked);
