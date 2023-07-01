using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymJournal.Interface.Data.Migrations
{
    /// <inheritdoc />
    public partial class workSetFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSets_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkSets_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"),
                column: "Password",
                value: "$2a$11$WnQ466XOcR.TMa0QJ2fBm.ev8HhQG.NvyKD/BE5PuMeYrd7meb9ce");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSets_ExerciseId",
                table: "WorkSets",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSets_UserId",
                table: "WorkSets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSets");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"),
                column: "Password",
                value: "$2a$11$2vY5cml5zqY34Q8ZppZ4cepzvTz81bHc2PxKIStZAIoMIBRnik172");
        }
    }
}
