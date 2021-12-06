using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Oglasi",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
