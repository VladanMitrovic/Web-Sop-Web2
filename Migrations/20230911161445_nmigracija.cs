using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web2projekat.Migrations
{
    /// <inheritdoc />
    public partial class nmigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cena",
                table: "Narudzbina",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ImeArtikla",
                table: "Narudzbina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImeKupca",
                table: "Narudzbina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImeProdavca",
                table: "Narudzbina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Komentar",
                table: "Narudzbina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PordavacId",
                table: "Narudzbina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VerifikacijaStatus",
                table: "Korisnik",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Cena",
                table: "Artikal",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cena",
                table: "Narudzbina");

            migrationBuilder.DropColumn(
                name: "ImeArtikla",
                table: "Narudzbina");

            migrationBuilder.DropColumn(
                name: "ImeKupca",
                table: "Narudzbina");

            migrationBuilder.DropColumn(
                name: "ImeProdavca",
                table: "Narudzbina");

            migrationBuilder.DropColumn(
                name: "Komentar",
                table: "Narudzbina");

            migrationBuilder.DropColumn(
                name: "PordavacId",
                table: "Narudzbina");

            migrationBuilder.DropColumn(
                name: "VerifikacijaStatus",
                table: "Korisnik");

            migrationBuilder.AlterColumn<float>(
                name: "Cena",
                table: "Artikal",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
