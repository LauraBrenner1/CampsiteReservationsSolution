namespace CampsiteReservationsApi.Services;

public class SystemTime : ISystemTime
{

    public DateTime GetCurrent()
    {
        return DateTime.Now;
    }
}
