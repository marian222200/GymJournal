using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymJournal.Interface.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameMusclesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscle_MuscleGroups_MusclesId",
                table: "ExerciseMuscle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MuscleGroups",
                table: "MuscleGroups");

            migrationBuilder.RenameTable(
                name: "MuscleGroups",
                newName: "Muscles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscle_Muscles_MusclesId",
                table: "ExerciseMuscle",
                column: "MusclesId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscle_Muscles_MusclesId",
                table: "ExerciseMuscle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles");

            migrationBuilder.RenameTable(
                name: "Muscles",
                newName: "MuscleGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MuscleGroups",
                table: "MuscleGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscle_MuscleGroups_MusclesId",
                table: "ExerciseMuscle",
                column: "MusclesId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
