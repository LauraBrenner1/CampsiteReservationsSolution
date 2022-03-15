namespace CampsiteReservationsApi.Services;

public interface ILookupOnCallDevelopers
{
    Task<string> GetEmailAddressAsync();
}
