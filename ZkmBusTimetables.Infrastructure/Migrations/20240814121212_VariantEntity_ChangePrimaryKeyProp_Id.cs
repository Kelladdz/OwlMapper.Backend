using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VariantEntity_ChangePrimaryKeyProp_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_BusStops_BusStopId",
                table: "RouteStops");

            migrationBuilder.RenameColumn(
                name: "BusStopId",
                table: "RouteStops",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_BusStops_Id",
                table: "RouteStops",
                column: "Id",
                principalTable: "BusStops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_BusStops_Id",
                table: "RouteStops");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RouteStops",
                newName: "BusStopId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_BusStops_BusStopId",
                table: "RouteStops",
                column: "BusStopId",
                principalTable: "BusStops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
