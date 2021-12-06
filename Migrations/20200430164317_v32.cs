using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipProslave",
                table: "Termini",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipProslave",
                table: "Rezervacije",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipProslave",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "TipProslave",
                table: "Rezervacije");
        }
    }
}
