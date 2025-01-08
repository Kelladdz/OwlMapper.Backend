using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VariantEntity_ChangeProperty_SymbolToIsDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Variants");

            migrationBuilder.AddColumn<bool>(
                name: "Is Default",
                table: "Variants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is Default",
                table: "Variants");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Variants",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "");
        }
    }
}
