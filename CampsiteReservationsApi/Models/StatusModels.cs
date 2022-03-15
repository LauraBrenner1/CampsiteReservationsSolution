namespace CampsiteReservationsApi.Models;

public record GetStatusResponse(string status, string oncall, DateTime whenChecked);