using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace oMeli_Back.Migrations
{
    /// <inheritdoc />
    public partial class addedplanentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PublicationLimited = table.Column<int>(type: "int", nullable: false),
                    PublicationCustom = table.Column<bool>(type: "bit", nullable: false),
                    StoreCustom = table.Column<bool>(type: "bit", nullable: false),
                    PublicationUnlimited = table.Column<bool>(type: "bit", nullable: false),
                    ViewStatics = table.Column<bool>(type: "bit", nullable: false),
                    CSVImport = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Plan",
                columns: new[] { "Id", "CSVImport", "Name", "PublicationCustom", "PublicationLimited", "PublicationUnlimited", "StoreCustom", "ViewStatics" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), false, "Free", false, 5, false, false, false },
                    { new Guid("00000000-0000-0000-0000-000000000002"), false, "Pro", true, 15, false, true, false },
                    { new Guid("00000000-0000-0000-0000-000000000003"), true, "Premium", true, 15, true, true, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plan");
        }
    }
}
