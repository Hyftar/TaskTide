using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTideAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEvents_Category_CategoryId",
                table: "TaskEvents");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_TaskEvents_CategoryId",
                table: "TaskEvents");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TaskEvents");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Calendars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Calendars",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_ColorId",
                table: "Calendars",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_TaskEventColors_ColorId",
                table: "Calendars",
                column: "ColorId",
                principalTable: "TaskEventColors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_TaskEventColors_ColorId",
                table: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_ColorId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Calendars");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TaskEvents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_TaskEventColors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "TaskEventColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Category_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_CategoryId",
                table: "TaskEvents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ColorId",
                table: "Category",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_OwnerId",
                table: "Category",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEvents_Category_CategoryId",
                table: "TaskEvents",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
