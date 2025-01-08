using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DepartureEntity_AddProperties_IsOnlyInSchoolDays_IsOnlyInDaysWithoutSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnlyInDaysWithoutSchool",
                table: "Departures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlyInSchoolDays",
                table: "Departures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnlyInDaysWithoutSchool",
                table: "Departures");

            migrationBuilder.DropColumn(
                name: "IsOnlyInSchoolDays",
                table: "Departures");
        }
    }
}
