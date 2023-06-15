using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymJournal.Interface.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLikesFromExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Exercises");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"),
                column: "Password",
                value: "$2a$11$2vY5cml5zqY34Q8ZppZ4cepzvTz81bHc2PxKIStZAIoMIBRnik172");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"),
                column: "Password",
                value: "$2a$11$/LczuNMEzUqYtdPOiHoE6.EMXyydBl9nPCjR/XWtRf0fs5w4B77te");
        }
    }
}
