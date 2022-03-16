namespace CampsiteReservationsApi.Services;

public interface ILookupApiStatus
{
    Task<string> GetCurrentStatusAsync();
}
