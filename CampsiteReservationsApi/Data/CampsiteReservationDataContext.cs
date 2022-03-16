using Microsoft.EntityFrameworkCore;

namespace CampsiteReservationsApi.Data;

public class CampsiteReservationDataContext : DbContext
{
    public CampsiteReservationDataContext(DbContextOptions<CampsiteReservationDataContext> options): base(options)
    {
            
    }
    public DbSet<StatusInformation>? StatusInformation { get; set; }

}

public class StatusInformation
{
    public int Id { get; set; } 
    public string Status { get; set; } = string.Empty;
    public DateTime CheckedAt { get; set; }
}