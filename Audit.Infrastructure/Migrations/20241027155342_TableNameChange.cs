using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Audit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityFieldContentChanges");

            migrationBuilder.CreateTable(
                name: "DbContentChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    OldContent = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    NewContent = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    ChangedBy = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbContentChanges", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbContentChanges");

            migrationBuilder.CreateTable(
                name: "EntityFieldContentChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    NewContent = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    OldContent = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityFieldContentChanges", x => x.Id);
                });
        }
    }
}
