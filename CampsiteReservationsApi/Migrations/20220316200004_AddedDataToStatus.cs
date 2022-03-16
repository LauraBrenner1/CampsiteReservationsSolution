using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampsiteReservationsApi.Migrations
{
    public partial class AddedDataToStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubOfUpdated",
                table: "StatusInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "StatusInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubOfUpdated",
                table: "StatusInformation");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "StatusInformation");
        }
    }
}
