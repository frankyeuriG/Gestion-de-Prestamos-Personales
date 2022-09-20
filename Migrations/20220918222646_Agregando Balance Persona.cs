using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPrestamos2022.Migrations
{
    public partial class AgregandoBalancePersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Balance",
                table: "Personas",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Personas");
        }
    }
}
