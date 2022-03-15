using CampsiteReservationsApi.Models;

namespace CampsiteReservationsApi.IntegrationTests;

public static class TestObjects
{
    public static GetStatusResponse GetStubbedStatusResponse()
    {
        // something a little shady here but I'll talk about it later.
        return new GetStatusResponse("Looks Good", "Bob", new System.DateTime(1969, 4, 20, 23, 59, 00));
    }
}
