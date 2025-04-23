using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oMeli_Back.Migrations
{
    /// <inheritdoc />
    public partial class updated_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateRenovation",
                table: "Subscription",
                newName: "DateCreation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreation",
                table: "Subscription",
                newName: "DateRenovation");
        }
    }
}
