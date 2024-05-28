using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Supermarket.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producatori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaraOrigine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producatori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizatori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeUtilizator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipUtilizator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizatori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodBare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    ProducatorId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produse_Categorii_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produse_Producatori_ProducatorId",
                        column: x => x.ProducatorId,
                        principalTable: "Producatori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bonuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEliberare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CasierId = table.Column<int>(type: "int", nullable: false),
                    SumaIncasata = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonuri_Utilizatori_CasierId",
                        column: x => x.CasierId,
                        principalTable: "Utilizatori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdusId = table.Column<int>(type: "int", nullable: false),
                    Cantitate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitateMasura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAprovizionare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpirare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PretAchizitie = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PretVanzare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocuri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocuri_Produse_ProdusId",
                        column: x => x.ProdusId,
                        principalTable: "Produse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonProduse",
                columns: table => new
                {
                    BonId = table.Column<int>(type: "int", nullable: false),
                    ProdusId = table.Column<int>(type: "int", nullable: false),
                    Cantitate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonProduse", x => new { x.BonId, x.ProdusId });
                    table.ForeignKey(
                        name: "FK_BonProduse_Bonuri_BonId",
                        column: x => x.BonId,
                        principalTable: "Bonuri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonProduse_Produse_ProdusId",
                        column: x => x.ProdusId,
                        principalTable: "Produse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Utilizatori",
                columns: new[] { "Id", "NumeUtilizator", "Parola", "TipUtilizator" },
                values: new object[,]
                {
                    { 1, "admin", "admin", "admin" },
                    { 2, "casier", "casier", "casier" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonProduse_ProdusId",
                table: "BonProduse",
                column: "ProdusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuri_CasierId",
                table: "Bonuri",
                column: "CasierId");

            migrationBuilder.CreateIndex(
                name: "IX_Produse_CategoriaId",
                table: "Produse",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produse_ProducatorId",
                table: "Produse",
                column: "ProducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocuri_ProdusId",
                table: "Stocuri",
                column: "ProdusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonProduse");

            migrationBuilder.DropTable(
                name: "Stocuri");

            migrationBuilder.DropTable(
                name: "Bonuri");

            migrationBuilder.DropTable(
                name: "Produse");

            migrationBuilder.DropTable(
                name: "Utilizatori");

            migrationBuilder.DropTable(
                name: "Categorii");

            migrationBuilder.DropTable(
                name: "Producatori");
        }
    }
}
