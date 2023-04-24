using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaturitaPVACSharp.Migrations
{
    /// <inheritdoc />
    public partial class RozsireniDatabazeOClanky : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clanky",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nadpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clanky", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clanky_Uzivatele_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzivatele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clanky_AutorId",
                table: "Clanky",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clanky");
        }
    }
}
