using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_AspNetUsers_WebApp1UserId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_WebApp1UserId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "WebApp1UserId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "BrojClanovaBenda",
                table: "Oglasi");

            migrationBuilder.AddColumn<string>(
                name: "KorisnikIdId",
                table: "Rezervacije",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojClanova",
                table: "Oglasi",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_KorisnikIdId",
                table: "Rezervacije",
                column: "KorisnikIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_AspNetUsers_KorisnikIdId",
                table: "Rezervacije",
                column: "KorisnikIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_AspNetUsers_KorisnikIdId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_KorisnikIdId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "KorisnikIdId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "BrojClanova",
                table: "Oglasi");

            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Rezervacije",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebApp1UserId",
                table: "Rezervacije",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojClanovaBenda",
                table: "Oglasi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_WebApp1UserId",
                table: "Rezervacije",
                column: "WebApp1UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_AspNetUsers_WebApp1UserId",
                table: "Rezervacije",
                column: "WebApp1UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
