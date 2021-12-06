using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Termini",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Rezervacije",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Rezervacije");
        }
    }
}
