using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrojGosta",
                table: "Termini",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Termini",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojGosta",
                table: "Rezervacije",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_AspNetUsers_KorisnikId",
                table: "Termini");

            migrationBuilder.DropIndex(
                name: "IX_Termini_KorisnikId",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "BrojGosta",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "BrojGosta",
                table: "Rezervacije");
        }
    }
}
