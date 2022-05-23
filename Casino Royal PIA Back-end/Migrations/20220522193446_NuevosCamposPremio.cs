using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Casino_Royal_PIA_Back_end.Migrations
{
    public partial class NuevosCamposPremio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionPremio",
                table: "Premios",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionPremio",
                table: "Premios");
        }
    }
}
