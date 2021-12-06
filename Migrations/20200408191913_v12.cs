using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oglasi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    BrojClanova = table.Column<int>(nullable: false),
                    BrojGostiju = table.Column<int>(nullable: false),
                    BrojSala = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    TipOglasa = table.Column<string>(nullable: true),
                    VrstaMuzike = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglasi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    RezervacijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    KorisnikId1 = table.Column<string>(nullable: true),
                    OglasId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.RezervacijaId);
                    table.ForeignKey(
                        name: "FK_Rezervacije_AspNetUsers_KorisnikId1",
                        column: x => x.KorisnikId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_KorisnikId1",
                table: "Rezervacije",
                column: "KorisnikId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_OglasId",
                table: "Rezervacije",
                column: "OglasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "Oglasi");
        }
    }
}
