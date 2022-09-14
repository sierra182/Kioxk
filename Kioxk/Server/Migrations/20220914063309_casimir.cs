using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kioxk.Server.Migrations
{
    public partial class casimir : Migration
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
                name: "Hashset",
                columns: table => new
                {
                    HashsetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LivretId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashset", x => x.HashsetId);
                    table.ForeignKey(
                        name: "FK_Hashset_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId");
                    table.ForeignKey(
                        name: "FK_Hashset_Livret_LivretId",
                        column: x => x.LivretId,
                        principalTable: "Livret",
                        principalColumn: "LivretId");
                });

            migrationBuilder.CreateTable(
                name: "Int",
                columns: table => new
                {
                    IntId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    index = table.Column<int>(type: "INTEGER", nullable: false),
                    it = table.Column<int>(type: "INTEGER", nullable: false),
                    LivretId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Int", x => x.IntId);
                    table.ForeignKey(
                        name: "FK_Int_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId");
                    table.ForeignKey(
                        name: "FK_Int_Livret_LivretId",
                        column: x => x.LivretId,
                        principalTable: "Livret",
                        principalColumn: "LivretId");
                });

            migrationBuilder.CreateTable(
                name: "Datetime",
                columns: table => new
                {
                    DatetimeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    index = table.Column<int>(type: "INTEGER", nullable: false),
                    dt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LivretId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: true),
                    HashsetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datetime", x => x.DatetimeId);
                    table.ForeignKey(
                        name: "FK_Datetime_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId");
                    table.ForeignKey(
                        name: "FK_Datetime_Hashset_HashsetId",
                        column: x => x.HashsetId,
                        principalTable: "Hashset",
                        principalColumn: "HashsetId");
                    table.ForeignKey(
                        name: "FK_Datetime_Livret_LivretId",
                        column: x => x.LivretId,
                        principalTable: "Livret",
                        principalColumn: "LivretId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Datetime_CommandeId",
                table: "Datetime",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Datetime_HashsetId",
                table: "Datetime",
                column: "HashsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Datetime_LivretId",
                table: "Datetime",
                column: "LivretId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashset_CommandeId",
                table: "Hashset",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashset_LivretId",
                table: "Hashset",
                column: "LivretId");

            migrationBuilder.CreateIndex(
                name: "IX_Int_CommandeId",
                table: "Int",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Int_LivretId",
                table: "Int",
                column: "LivretId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datetime");

            migrationBuilder.DropTable(
                name: "Int");

            migrationBuilder.DropTable(
                name: "Hashset");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Livret");
        }
    }
}
