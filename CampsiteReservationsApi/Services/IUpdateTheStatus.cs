namespace CampsiteReservationsApi.Services;

public interface IUpdateTheStatus
{
    Task UpdateStatusAsync(string status, string sub, string userName);
}
