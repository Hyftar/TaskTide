using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTideAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskEventColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Red = table.Column<int>(type: "integer", nullable: false),
                    Green = table.Column<int>(type: "integer", nullable: false),
                    Blue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEventColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TransactionnalEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: false),
                    ModelId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionnalEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionnalEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CalendarInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InviterId = table.Column<int>(type: "integer", nullable: false),
                    InviteeId = table.Column<int>(type: "integer", nullable: false),
                    CalendarId = table.Column<int>(type: "integer", nullable: false),
                    Accepted = table.Column<bool>(type: "boolean", nullable: true),
                    RespondedAt = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarInvitations_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarInvitations_Users_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarInvitations_Users_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarUser",
                columns: table => new
                {
                    SharedCalendarsId = table.Column<int>(type: "integer", nullable: false),
                    SharedWithId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarUser", x => new { x.SharedCalendarsId, x.SharedWithId });
                    table.ForeignKey(
                        name: "FK_CalendarUser_Calendars_SharedCalendarsId",
                        column: x => x.SharedCalendarsId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarUser_Users_SharedWithId",
                        column: x => x.SharedWithId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CalendarId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEvents_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskEvents_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recurrences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskEventId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<LocalDate>(type: "date", nullable: false),
                    EndType = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: true),
                    Duration = table.Column<Period>(type: "interval", nullable: true),
                    Interval = table.Column<int>(type: "integer", nullable: true),
                    Ordinal = table.Column<int>(type: "integer", nullable: false),
                    Weekdays = table.Column<int>(type: "integer", nullable: false),
                    Months = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recurrences_TaskEvents_TaskEventId",
                        column: x => x.TaskEventId,
                        principalTable: "TaskEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskEventsInstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<LocalDate>(type: "date", nullable: false),
                    StartTime = table.Column<LocalTime>(type: "time", nullable: true),
                    Duration = table.Column<Duration>(type: "interval", nullable: true),
                    CreatedAt = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    AllDay = table.Column<bool>(type: "boolean", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEventsInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEventsInstance_TaskEvents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TaskEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarInvitations_CalendarId",
                table: "CalendarInvitations",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarInvitations_InviteeId",
                table: "CalendarInvitations",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarInvitations_InviterId",
                table: "CalendarInvitations",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_OwnerId",
                table: "Calendars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarUser_SharedWithId",
                table: "CalendarUser",
                column: "SharedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ColorId",
                table: "Category",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_OwnerId",
                table: "Category",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recurrences_TaskEventId",
                table: "Recurrences",
                column: "TaskEventId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_CalendarId",
                table: "TaskEvents",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_CategoryId",
                table: "TaskEvents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEventsInstance_ParentId",
                table: "TaskEventsInstance",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionnalEvents_UserId",
                table: "TransactionnalEvents",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarInvitations");

            migrationBuilder.DropTable(
                name: "CalendarUser");

            migrationBuilder.DropTable(
                name: "Recurrences");

            migrationBuilder.DropTable(
                name: "TaskEventsInstance");

            migrationBuilder.DropTable(
                name: "TransactionnalEvents");

            migrationBuilder.DropTable(
                name: "TaskEvents");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "TaskEventColors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
