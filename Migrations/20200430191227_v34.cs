using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_AspNetUsers_KorisnikId",
                table: "Termini");

            migrationBuilder.DropIndex(
                name: "IX_Termini_KorisnikId",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Termini");

            migrationBuilder.AddColumn<string>(
                name: "KorisnikBroj",
                table: "Termini",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KorisnikImePrezime",
                table: "Termini",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KorisnikBroj",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "KorisnikImePrezime",
                table: "Termini");

            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Termini",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Termini_KorisnikId",
                table: "Termini",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Termini_AspNetUsers_KorisnikId",
                table: "Termini",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
