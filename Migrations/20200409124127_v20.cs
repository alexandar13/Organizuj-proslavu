using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termin_Oglasi_OglasId",
                table: "Termin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Termin",
                table: "Termin");

            migrationBuilder.RenameTable(
                name: "Termin",
                newName: "Termini");

            migrationBuilder.RenameIndex(
                name: "IX_Termin_OglasId",
                table: "Termini",
                newName: "IX_Termini_OglasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Termini",
                table: "Termini",
                column: "TerminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Termini_Oglasi_OglasId",
                table: "Termini",
                column: "OglasId",
                principalTable: "Oglasi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_Oglasi_OglasId",
                table: "Termini");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Termini",
                table: "Termini");

            migrationBuilder.RenameTable(
                name: "Termini",
                newName: "Termin");

            migrationBuilder.RenameIndex(
                name: "IX_Termini_OglasId",
                table: "Termin",
                newName: "IX_Termin_OglasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Termin",
                table: "Termin",
                column: "TerminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_Oglasi_OglasId",
                table: "Termin",
                column: "OglasId",
                principalTable: "Oglasi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
