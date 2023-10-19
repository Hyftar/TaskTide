using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTideAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddParentAttributeToTaskEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEvents_Calendars_CalendarId",
                table: "TaskEvents");

            migrationBuilder.DropIndex(
                name: "IX_TaskEvents_CalendarId",
                table: "TaskEvents");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "TaskEvents");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "TaskEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_ParentId",
                table: "TaskEvents",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEvents_Calendars_ParentId",
                table: "TaskEvents",
                column: "ParentId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEvents_Calendars_ParentId",
                table: "TaskEvents");

            migrationBuilder.DropIndex(
                name: "IX_TaskEvents_ParentId",
                table: "TaskEvents");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TaskEvents");

            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "TaskEvents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_CalendarId",
                table: "TaskEvents",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEvents_Calendars_CalendarId",
                table: "TaskEvents",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id");
        }
    }
}
