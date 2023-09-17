using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web2projekat.Migrations
{
    /// <inheritdoc />
    public partial class nmigracija1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotografija",
                table: "Artikal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fotografija",
                table: "Artikal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
