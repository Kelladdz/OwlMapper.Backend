using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CoordinateEntity_ChangeProperty_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinate_Longitude",
                table: "RouteLinePoints",
                newName: "Coordinate_Lng");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Latitude",
                table: "RouteLinePoints",
                newName: "Coordinate_Lat");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Longitude",
                table: "BusStops",
                newName: "Coordinate_Lng");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Latitude",
                table: "BusStops",
                newName: "Coordinate_Lat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinate_Lng",
                table: "RouteLinePoints",
                newName: "Coordinate_Longitude");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Lat",
                table: "RouteLinePoints",
                newName: "Coordinate_Latitude");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Lng",
                table: "BusStops",
                newName: "Coordinate_Longitude");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Lat",
                table: "BusStops",
                newName: "Coordinate_Latitude");
        }
    }
}
