using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oglasi_AspNetUsers_KorisnikId",
                table: "Oglasi");

            migrationBuilder.DropIndex(
                name: "IX_Oglasi_KorisnikId",
                table: "Oglasi");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Oglasi");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Oglasi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Oglasi",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Termin",
                columns: table => new
                {
                    TerminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OglasId = table.Column<int>(nullable: true),
                    ZauzetTermin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termin", x => x.TerminId);
                    table.ForeignKey(
                        name: "FK_Termin_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Termin_OglasId",
                table: "Termin",
                column: "OglasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Termin");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Oglasi");

            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Oglasi");

            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Oglasi",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oglasi_KorisnikId",
                table: "Oglasi",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Oglasi_AspNetUsers_KorisnikId",
                table: "Oglasi",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
