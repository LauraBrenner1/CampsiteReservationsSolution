using CampsiteReservationsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CampsiteReservationsApi.Services;

public class SqlServerStatus : ILookupApiStatus, IUpdateTheStatus
{
    private readonly CampsiteReservationDataContext _context;
    private readonly ISystemTime _systemTime;
    public SqlServerStatus(CampsiteReservationDataContext context, ISystemTime systemTime)
    {
        _context = context;
        _systemTime = systemTime;
    }

    public async Task<string?> GetCurrentStatusAsync()
    {
        // find the status message of the status row that has the most recent checkedattime
        var lastStatus = await _context.StatusInformation?
            .OrderByDescending(s => s.CheckedAt)
            .Select(s => s.Status)
            .FirstOrDefaultAsync();

        return lastStatus; // This is sus according to my test.
    }

    public async Task UpdateStatusAsync(string status, string sub, string userName)
    {
        var statusRecord = new StatusInformation
        {
            Status = status,
            CheckedAt = _systemTime.GetCurrent(),
            SubOfUpdated = sub,
            UpdatedBy = userName
        };

        _context.StatusInformation?.Add(statusRecord);

        await _context.SaveChangesAsync();
    }
}
