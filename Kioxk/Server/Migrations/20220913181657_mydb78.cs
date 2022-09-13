using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kioxk.Server.Migrations
{
    public partial class mydb78 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ref = table.Column<long>(type: "INTEGER", nullable: false),
                    Valide = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<long>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    RgtsCompl = table.Column<string>(type: "TEXT", nullable: true),
                    Total = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.CommandeId);
                });

            migrationBuilder.CreateTable(
                name: "Livret",
                columns: table => new
                {
                    LivretId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livret", x => x.LivretId);
                });

            migrationBuilder.CreateTable(
                name: "Hashsets",
                columns: table => new
                {
                    HashsetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LivretId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashsets", x => x.HashsetId);
                    table.ForeignKey(
                        name: "FK_Hashsets_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hashsets_Livret_LivretId",
                        column: x => x.LivretId,
                        principalTable: "Livret",
                        principalColumn: "LivretId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ints",
                columns: table => new
                {
                    IntId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    index = table.Column<int>(type: "INTEGER", nullable: false),
                    it = table.Column<int>(type: "INTEGER", nullable: false),
                    LivretId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ints", x => x.IntId);
                    table.ForeignKey(
                        name: "FK_Ints_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ints_Livret_LivretId",
                        column: x => x.LivretId,
                        principalTable: "Livret",
                        principalColumn: "LivretId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Datetimes",
                columns: table => new
                {
                    DatetimeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    index = table.Column<int>(type: "INTEGER", nullable: false),
                    dt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LivretId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: false),
                    HashsetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datetimes", x => x.DatetimeId);
                    table.ForeignKey(
                        name: "FK_Datetimes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Datetimes_Hashsets_HashsetId",
                        column: x => x.HashsetId,
                        principalTable: "Hashsets",
                        principalColumn: "HashsetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Datetimes_Livret_LivretId",
                        column: x => x.LivretId,
                        principalTable: "Livret",
                        principalColumn: "LivretId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Datetimes_CommandeId",
                table: "Datetimes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Datetimes_HashsetId",
                table: "Datetimes",
                column: "HashsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Datetimes_LivretId",
                table: "Datetimes",
                column: "LivretId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashsets_CommandeId",
                table: "Hashsets",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashsets_LivretId",
                table: "Hashsets",
                column: "LivretId");

            migrationBuilder.CreateIndex(
                name: "IX_Ints_CommandeId",
                table: "Ints",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ints_LivretId",
                table: "Ints",
                column: "LivretId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datetimes");

            migrationBuilder.DropTable(
                name: "Ints");

            migrationBuilder.DropTable(
                name: "Hashsets");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Livret");
        }
    }
}
