using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizujProslavu.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojGostiju",
                table: "Oglasi");

            migrationBuilder.AddColumn<int>(
                name: "BrojDece",
                table: "Oglasi",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojGosta",
                table: "Oglasi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Oglasi",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Karakteristike",
                columns: table => new
                {
                    KarakteristikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    OglasId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karakteristike", x => x.KarakteristikeId);
                    table.ForeignKey(
                        name: "FK_Karakteristike_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oglasi_KorisnikId",
                table: "Oglasi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Karakteristike_OglasId",
                table: "Karakteristike",
                column: "OglasId");

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

            migrationBuilder.DropTable(
                name: "Karakteristike");

            migrationBuilder.DropIndex(
                name: "IX_Oglasi_KorisnikId",
                table: "Oglasi");

            migrationBuilder.DropColumn(
                name: "BrojDece",
                table: "Oglasi");

            migrationBuilder.DropColumn(
                name: "BrojGosta",
                table: "Oglasi");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Oglasi");

            migrationBuilder.AddColumn<int>(
                name: "BrojGostiju",
                table: "Oglasi",
                type: "int",
                nullable: true);
        }
    }
}
