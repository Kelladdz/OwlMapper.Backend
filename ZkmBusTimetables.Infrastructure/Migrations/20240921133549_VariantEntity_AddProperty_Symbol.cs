using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VariantEntity_AddProperty_Symbol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Variants",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Variants");
        }
    }
}
