using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymJournal.Data.Migrations
{
    /// <inheritdoc />
    public partial class userChooseWorkout6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseMuscle",
                columns: table => new
                {
                    ExercisesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusclesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMuscle", x => new { x.ExercisesId, x.MusclesId });
                    table.ForeignKey(
                        name: "FK_ExerciseMuscle_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseMuscle_Muscles_MusclesId",
                        column: x => x.MusclesId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkoutPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkoutPlanStart = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseWorkout",
                columns: table => new
                {
                    ExercisesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWorkout", x => new { x.ExercisesId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutWorkoutPlan",
                columns: table => new
                {
                    WorkoutPlansId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutWorkoutPlan", x => new { x.WorkoutPlansId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_WorkoutWorkoutPlan_WorkoutPlans_WorkoutPlansId",
                        column: x => x.WorkoutPlansId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutWorkoutPlan_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3c0b0369-e88c-40f0-bc30-a1a309958db4"), "Biceps" },
                    { new Guid("3cf1dc75-d641-4305-97e5-eb629f454676"), "Chest" },
                    { new Guid("3ef37065-e38e-4676-a0da-af322e750d45"), "Quads" },
                    { new Guid("576d342b-b493-4edc-ad56-fffd2ffa36fb"), "LateralDelt" },
                    { new Guid("5a25a051-75d3-4733-839c-f645ca8f20f7"), "Back" },
                    { new Guid("6328df6e-3f56-4ad6-ba7b-50ab92270b48"), "RearDelt" },
                    { new Guid("8bd9fdf8-f0b5-4361-b447-b926195637c7"), "FrontalDelt" },
                    { new Guid("909c35a3-a2c0-4fc9-97b6-5299a0d88dab"), "Forearms" },
                    { new Guid("a2d5742d-a68a-4b70-a7ac-94f31dd77b03"), "Calves" },
                    { new Guid("a8627c30-a7b9-4b2c-af13-350b77faa36b"), "Other" },
                    { new Guid("dc0d5865-8fa7-48a1-89cc-65986e10928e"), "Hamstrings" },
                    { new Guid("f46d7cf7-a3a8-42eb-97c5-0e3b10b5bbe1"), "Abs" },
                    { new Guid("f62031db-81a1-4643-8e44-845c19eb51e4"), "Triceps" }
                });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "Name", "Password", "Role", "Token", "WorkoutPlanId", "WorkoutPlanStart" },
                values: new object[] { new Guid("42282faf-05a4-48ff-b062-65fed7b5e84a"), "Admin", "$2a$11$UGVpOHQKT1mMw5YLrCJ4A.P1RWVvkZ0.ItcTb146/e5yoVvDwd7xS", "Admin", new Guid("8ae01d7d-3965-4b7e-b8af-e12fd5f588f6"), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMuscle_MusclesId",
                table: "ExerciseMuscle",
                column: "MusclesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkout_WorkoutsId",
                table: "ExerciseWorkout",
                column: "WorkoutsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Name",
                table: "UserInfos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_WorkoutPlanId",
                table: "UserInfos",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutWorkoutPlan_WorkoutsId",
                table: "WorkoutWorkoutPlan",
                column: "WorkoutsId");

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
                name: "ExerciseMuscle");

            migrationBuilder.DropTable(
                name: "ExerciseWorkout");

            migrationBuilder.DropTable(
                name: "WorkoutWorkoutPlan");

            migrationBuilder.DropTable(
                name: "WorkSets");

            migrationBuilder.DropTable(
                name: "Muscles");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");
        }
    }
}
