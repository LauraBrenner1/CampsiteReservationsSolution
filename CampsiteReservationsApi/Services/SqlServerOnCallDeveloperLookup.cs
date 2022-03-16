using CampsiteReservationsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CampsiteReservationsApi.Services;

public class SqlServerOnCallDeveloperLookup : ILookupApiStatus
{
    private readonly CampsiteReservationDataContext _context;

    public SqlServerOnCallDeveloperLookup(CampsiteReservationDataContext context)
    {
        _context = context;
    }

    public async Task<string> GetCurrentStatusAsync()
    {
        // find the status message of the status row that has the most recent checkedattime
        var lastStatus = await _context.StatusInformation?
            .OrderByDescending(s => s.CheckedAt)
            .Select(s => s.Status)
            .FirstOrDefaultAsync();

        return lastStatus ?? "No Status Information Recorded";
    }
}
