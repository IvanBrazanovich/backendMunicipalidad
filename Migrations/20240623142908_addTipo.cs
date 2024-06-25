using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMunicipalidadTP.Migrations
{
    /// <inheritdoc />
    public partial class addTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Propietario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Propietario");
        }
    }
}
