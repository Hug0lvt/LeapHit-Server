using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class mymigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    playerId = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    nbBallTouchTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    timePlayed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.playerId);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    chatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    player1 = table.Column<string>(type: "TEXT", nullable: false),
                    player2 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.chatId);
                    table.ForeignKey(
                        name: "FK_Chats_Players_player1",
                        column: x => x.player1,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Players_player2",
                        column: x => x.player2,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    gameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    durationGame = table.Column<int>(type: "INTEGER", nullable: false),
                    nbMaxEchanges = table.Column<int>(type: "INTEGER", nullable: false),
                    winner = table.Column<string>(type: "TEXT", nullable: false),
                    loser = table.Column<string>(type: "TEXT", nullable: false),
                    scoreWinner = table.Column<int>(type: "INTEGER", nullable: false),
                    scoreLoser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.gameId);
                    table.ForeignKey(
                        name: "FK_Games_Players_loser",
                        column: x => x.loser,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_winner",
                        column: x => x.winner,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    messageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    player = table.Column<string>(type: "TEXT", nullable: false),
                    chat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.messageId);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_chat",
                        column: x => x.chat,
                        principalTable: "Chats",
                        principalColumn: "chatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Players_player",
                        column: x => x.player,
                        principalTable: "Players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_player1",
                table: "Chats",
                column: "player1");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_player2",
                table: "Chats",
                column: "player2");

            migrationBuilder.CreateIndex(
                name: "IX_Games_loser",
                table: "Games",
                column: "loser");

            migrationBuilder.CreateIndex(
                name: "IX_Games_winner",
                table: "Games",
                column: "winner");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_chat",
                table: "Messages",
                column: "chat");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_player",
                table: "Messages",
                column: "player");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
