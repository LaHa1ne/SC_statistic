using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC_statistic.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "corporations",
                columns: table => new
                {
                    CorporationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrentTag = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PvpRating = table.Column<int>(type: "int", nullable: false),
                    PveRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corporations", x => x.CorporationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.NotificationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Login = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    PlayerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrentNickname = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsInformationCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CurrentCorporationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_players_corporations_CurrentCorporationId",
                        column: x => x.CurrentCorporationId,
                        principalTable: "corporations",
                        principalColumn: "CorporationId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "playercorporationhistories",
                columns: table => new
                {
                    PlayerCorporationHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    CorporationId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playercorporationhistories", x => x.PlayerCorporationHistoryId);
                    table.ForeignKey(
                        name: "FK_playercorporationhistories_corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "corporations",
                        principalColumn: "CorporationId");
                    table.ForeignKey(
                        name: "FK_playercorporationhistories_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "playernicknamehistories",
                columns: table => new
                {
                    PlayerNicknameHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    Nickname = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playernicknamehistories", x => x.PlayerNicknameHistoryId);
                    table.ForeignKey(
                        name: "FK_playernicknamehistories_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trackedplayers",
                columns: table => new
                {
                    TrackedPlayerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trackedplayers", x => x.TrackedPlayerId);
                    table.ForeignKey(
                        name: "FK_trackedplayers_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trackedplayers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TrackedPlayerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_sessions_trackedplayers_TrackedPlayerId",
                        column: x => x.TrackedPlayerId,
                        principalTable: "trackedplayers",
                        principalColumn: "TrackedPlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "checkpoints",
                columns: table => new
                {
                    CheckpointId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsStarted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CheckpointStat_GamePlayed = table.Column<int>(type: "int", nullable: false),
                    CheckpointStat_GameWin = table.Column<int>(type: "int", nullable: false),
                    CheckpointStat_TotalAssists = table.Column<int>(type: "int", nullable: false),
                    CheckpointStat_TotalDeath = table.Column<int>(type: "int", nullable: false),
                    CheckpointStat_TotalKill = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkpoints", x => x.CheckpointId);
                    table.ForeignKey(
                        name: "FK_checkpoints_sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_checkpoints_SessionId_Date",
                table: "checkpoints",
                columns: new[] { "SessionId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_corporations_CurrentName",
                table: "corporations",
                column: "CurrentName");

            migrationBuilder.CreateIndex(
                name: "IX_corporations_CurrentTag",
                table: "corporations",
                column: "CurrentTag");

            migrationBuilder.CreateIndex(
                name: "Date_index",
                table: "notifications",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_playercorporationhistories_CorporationId",
                table: "playercorporationhistories",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_playercorporationhistories_PlayerId_Date",
                table: "playercorporationhistories",
                columns: new[] { "PlayerId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_playernicknamehistories_PlayerId_Date",
                table: "playernicknamehistories",
                columns: new[] { "PlayerId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_CurrentCorporationId",
                table: "players",
                column: "CurrentCorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_players_CurrentNickname_IsInformationCorrect",
                table: "players",
                columns: new[] { "CurrentNickname", "IsInformationCorrect" });

            migrationBuilder.CreateIndex(
                name: "IX_sessions_TrackedPlayerId_StartDate",
                table: "sessions",
                columns: new[] { "TrackedPlayerId", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_trackedplayers_PlayerId",
                table: "trackedplayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_trackedplayers_UserId_PlayerId",
                table: "trackedplayers",
                columns: new[] { "UserId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Login_UNIQUE",
                table: "users",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkpoints");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "playercorporationhistories");

            migrationBuilder.DropTable(
                name: "playernicknamehistories");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "trackedplayers");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "corporations");
        }
    }
}
