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
                    CurrentCorporationId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_players_corporations_CurrentCorporationId",
                        column: x => x.CurrentCorporationId,
                        principalTable: "corporations",
                        principalColumn: "CorporationId");
                    table.ForeignKey(
                        name: "FK_players_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
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

            migrationBuilder.InsertData(
                table: "corporations",
                columns: new[] { "CorporationId", "CurrentName", "CurrentTag", "PveRating", "PvpRating" },
                values: new object[,]
                {
                    { 4505L, "44cd", "4CB", 50, 10 },
                    { 5861L, "Lamento el Teaming", "LET", 12652, 19313 }
                });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "NotificationId", "Date", "Text", "Type" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 7, 17, 18, 50, 1, 737, DateTimeKind.Local).AddTicks(4646), "Системное1", 3 },
                    { 2L, new DateTime(2023, 7, 17, 18, 50, 2, 737, DateTimeKind.Local).AddTicks(4659), "Игрок1", 0 },
                    { 3L, new DateTime(2023, 7, 17, 18, 50, 3, 737, DateTimeKind.Local).AddTicks(4660), "Смена корп1", 2 },
                    { 4L, new DateTime(2023, 7, 17, 18, 50, 4, 737, DateTimeKind.Local).AddTicks(4661), "Смена назв1", 1 },
                    { 5L, new DateTime(2023, 7, 17, 18, 50, 5, 737, DateTimeKind.Local).AddTicks(4662), "Системное2", 3 },
                    { 6L, new DateTime(2023, 7, 17, 18, 50, 6, 737, DateTimeKind.Local).AddTicks(4665), "Игрок2", 0 },
                    { 7L, new DateTime(2023, 7, 17, 18, 50, 7, 737, DateTimeKind.Local).AddTicks(4666), "Смена корп2", 2 },
                    { 8L, new DateTime(2023, 7, 17, 18, 50, 8, 737, DateTimeKind.Local).AddTicks(4667), "Смена назв2", 1 },
                    { 9L, new DateTime(2023, 7, 17, 18, 50, 9, 737, DateTimeKind.Local).AddTicks(4668), "Системное3", 3 },
                    { 10L, new DateTime(2023, 7, 17, 18, 50, 10, 737, DateTimeKind.Local).AddTicks(4669), "Игрок3", 0 },
                    { 11L, new DateTime(2023, 7, 17, 18, 50, 11, 737, DateTimeKind.Local).AddTicks(4670), "Смена корп3", 2 },
                    { 12L, new DateTime(2023, 7, 17, 18, 50, 12, 737, DateTimeKind.Local).AddTicks(4671), "Смена назв3", 1 },
                    { 13L, new DateTime(2023, 7, 17, 18, 50, 13, 737, DateTimeKind.Local).AddTicks(4672), "Системное4", 3 },
                    { 14L, new DateTime(2023, 7, 17, 18, 50, 14, 737, DateTimeKind.Local).AddTicks(4673), "Игрок4", 0 },
                    { 15L, new DateTime(2023, 7, 17, 18, 50, 15, 737, DateTimeKind.Local).AddTicks(4674), "Смена корп4", 2 },
                    { 16L, new DateTime(2023, 7, 17, 18, 50, 16, 737, DateTimeKind.Local).AddTicks(4675), "Смена назв4", 1 }
                });

            migrationBuilder.InsertData(
                table: "players",
                columns: new[] { "PlayerId", "CurrentCorporationId", "CurrentNickname", "IsInformationCorrect", "UserId" },
                values: new object[] { 585650L, 4505L, "fantazm", true, null });

            migrationBuilder.InsertData(
                table: "players",
                columns: new[] { "PlayerId", "CurrentCorporationId", "CurrentNickname", "IsInformationCorrect", "UserId" },
                values: new object[] { 2177186L, 4505L, "AggressiveStyle", true, null });

            migrationBuilder.InsertData(
                table: "playercorporationhistories",
                columns: new[] { "PlayerCorporationHistoryId", "CorporationId", "Date", "PlayerId" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2014, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 585650L },
                    { 2L, 4505L, new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 585650L },
                    { 3L, 4505L, new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2177186L }
                });

            migrationBuilder.InsertData(
                table: "playernicknamehistories",
                columns: new[] { "PlayerNicknameHistoryId", "Date", "Nickname", "PlayerId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2015, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ala", 585650L },
                    { 2L, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fantazm", 585650L },
                    { 3L, new DateTime(2021, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "AggressiveStyle", 2177186L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_corporations_CurrentName",
                table: "corporations",
                column: "CurrentName",
                unique: true);

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
                name: "IX_players_UserId",
                table: "players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "Login_UNIQUE",
                table: "users",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "playercorporationhistories");

            migrationBuilder.DropTable(
                name: "playernicknamehistories");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "corporations");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
