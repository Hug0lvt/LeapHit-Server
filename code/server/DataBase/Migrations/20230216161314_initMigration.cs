using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    playerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    nbBallTouchTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    timePlayed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.playerId);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    chatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    player1 = table.Column<int>(type: "INTEGER", nullable: false),
                    player2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.chatId);
                    table.ForeignKey(
                        name: "FK_Chat_Player_player1",
                        column: x => x.player1,
                        principalTable: "Player",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_Player_player2",
                        column: x => x.player2,
                        principalTable: "Player",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    gameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    durationGame = table.Column<int>(type: "INTEGER", nullable: false),
                    nbMaxEchanges = table.Column<int>(type: "INTEGER", nullable: false),
                    winner = table.Column<int>(type: "INTEGER", nullable: false),
                    loser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.gameId);
                    table.ForeignKey(
                        name: "FK_Game_Player_loser",
                        column: x => x.loser,
                        principalTable: "Player",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Player_winner",
                        column: x => x.winner,
                        principalTable: "Player",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    player = table.Column<int>(type: "INTEGER", nullable: false),
                    chat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.messageId);
                    table.ForeignKey(
                        name: "FK_Message_Chat_chat",
                        column: x => x.chat,
                        principalTable: "Chat",
                        principalColumn: "chatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Player_player",
                        column: x => x.player,
                        principalTable: "Player",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "playerId", "name", "nbBallTouchTotal", "timePlayed" },
                values: new object[,]
                {
                    { 1, "Rami", 20, 120 },
                    { 2, "Hugo", 90, 250 }
                });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "chatId", "player1", "player2" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "gameId", "durationGame", "loser", "nbMaxEchanges", "winner" },
                values: new object[] { 1, 65, 2, 5, 1 });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "messageId", "chat", "message", "player", "timestamp" },
                values: new object[,]
                {
                    { 1, 1, "Salut mon gars !", 1, new DateTime(2023, 2, 16, 17, 5, 12, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Comment tu vas ?", 2, new DateTime(2023, 2, 16, 17, 12, 35, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_player1",
                table: "Chat",
                column: "player1");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_player2",
                table: "Chat",
                column: "player2");

            migrationBuilder.CreateIndex(
                name: "IX_Game_loser",
                table: "Game",
                column: "loser");

            migrationBuilder.CreateIndex(
                name: "IX_Game_winner",
                table: "Game",
                column: "winner");

            migrationBuilder.CreateIndex(
                name: "IX_Message_chat",
                table: "Message",
                column: "chat");

            migrationBuilder.CreateIndex(
                name: "IX_Message_player",
                table: "Message",
                column: "player");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
