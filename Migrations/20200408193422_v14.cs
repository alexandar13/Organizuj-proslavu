using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_AspNetUsers_KorisnikId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_KorisnikId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "BrojClanova",
                table: "Oglasi");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikId",
                table: "Rezervacije",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebApp1UserId",
                table: "Rezervacije",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojClanovaBenda",
                table: "Oglasi",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_AspNetUsers_WebApp1UserId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_WebApp1UserId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "WebApp1UserId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "BrojClanovaBenda",
                table: "Oglasi");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikId",
                table: "Rezervacije",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojClanova",
                table: "Oglasi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_KorisnikId",
                table: "Rezervacije",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_AspNetUsers_KorisnikId",
                table: "Rezervacije",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
