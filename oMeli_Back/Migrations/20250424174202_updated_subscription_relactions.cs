using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oMeli_Back.Migrations
{
    /// <inheritdoc />
    public partial class updated_subscription_relactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription");

            migrationBuilder.RenameColumn(
                name: "RenovationAutomatic",
                table: "Subscription",
                newName: "Renovation");

            migrationBuilder.RenameColumn(
                name: "PublicationUnlimited",
                table: "Plan",
                newName: "StoreCreate");

            migrationBuilder.RenameColumn(
                name: "PublicationLimited",
                table: "Plan",
                newName: "ProductLimited");

            migrationBuilder.AddColumn<bool>(
                name: "Priority",
                table: "Plan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Name", "Priority", "StoreCreate" },
                values: new object[] { "Basic", false, true });

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "Priority", "StoreCreate" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "Priority", "ProductLimited" },
                values: new object[] { true, 30 });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Plan");

            migrationBuilder.RenameColumn(
                name: "Renovation",
                table: "Subscription",
                newName: "RenovationAutomatic");

            migrationBuilder.RenameColumn(
                name: "StoreCreate",
                table: "Plan",
                newName: "PublicationUnlimited");

            migrationBuilder.RenameColumn(
                name: "ProductLimited",
                table: "Plan",
                newName: "PublicationLimited");

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Name", "PublicationUnlimited" },
                values: new object[] { "Free", false });

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "PublicationUnlimited",
                value: false);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "PublicationLimited",
                value: 15);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId");
        }
    }
}
