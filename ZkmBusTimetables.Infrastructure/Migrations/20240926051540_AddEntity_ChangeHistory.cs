using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntity_ChangeHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeHistory",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(name: "Entity Type", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EntityId = table.Column<Guid>(name: "Entity Id", type: "uniqueidentifier", nullable: false),
                    ChangeType = table.Column<string>(name: "Change Type", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChangedBy = table.Column<string>(name: "Changed By", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChangeDate = table.Column<DateTime>(name: "Change Date", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeHistory",
                schema: "identity");
        }
    }
}
