using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VariantEntity_RemoveProperty_RowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Variants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Variants",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
