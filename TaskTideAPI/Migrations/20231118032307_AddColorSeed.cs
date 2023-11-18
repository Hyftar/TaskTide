using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTideAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColorSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskEventColors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[,]
                {
                    { 1, (byte)203, (byte)128, "Default", (byte)92 },
                    { 2, (byte)79, (byte)150, "Orange", (byte)255 },
                    { 3, (byte)97, (byte)105, "Red", (byte)255 },
                    { 4, (byte)204, (byte)122, "Lavender", (byte)174 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskEventColors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskEventColors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskEventColors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskEventColors",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
