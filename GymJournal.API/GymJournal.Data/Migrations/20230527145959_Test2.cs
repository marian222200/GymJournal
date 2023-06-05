using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymJournal.Interface.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("3c0b0369-e88c-40f0-bc30-a1a309958db4"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("3ef37065-e38e-4676-a0da-af322e750d45"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("576d342b-b493-4edc-ad56-fffd2ffa36fb"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("5a25a051-75d3-4733-839c-f645ca8f20f7"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("6328df6e-3f56-4ad6-ba7b-50ab92270b48"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("8bd9fdf8-f0b5-4361-b447-b926195637c7"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("909c35a3-a2c0-4fc9-97b6-5299a0d88dab"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("a2d5742d-a68a-4b70-a7ac-94f31dd77b03"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("a8627c30-a7b9-4b2c-af13-350b77faa36b"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("dc0d5865-8fa7-48a1-89cc-65986e10928e"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("f46d7cf7-a3a8-42eb-97c5-0e3b10b5bbe1"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("f62031db-81a1-4643-8e44-845c19eb51e4"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3c0b0369-e88c-40f0-bc30-a1a309958db4"), "Biceps" },
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
        }
    }
}
