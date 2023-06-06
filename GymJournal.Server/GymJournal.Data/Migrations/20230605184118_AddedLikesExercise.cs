using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymJournal.Interface.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikesExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Exercises");
        }
    }
}
