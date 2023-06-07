using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymJournal.Interface.Data.Migrations
{
    /// <inheritdoc />
    public partial class usersConfigTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserInfos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "Name", "Password", "Role", "Token" },
                values: new object[] { new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"), "Admin", "$2a$11$/LczuNMEzUqYtdPOiHoE6.EMXyydBl9nPCjR/XWtRf0fs5w4B77te", "Admin", new Guid("8ae01d7d-3965-4b7e-b8af-e12fd5f588f6") });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Name",
                table: "UserInfos",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfos_Name",
                table: "UserInfos");

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
