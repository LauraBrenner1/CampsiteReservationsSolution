using System.ComponentModel.DataAnnotations;

namespace CampsiteReservationsApi.Models;

public record GetStatusResponse(string status, string oncall, DateTime whenChecked);

public record PostStatusRequest([Required] string status);