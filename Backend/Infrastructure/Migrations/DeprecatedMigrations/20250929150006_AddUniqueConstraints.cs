using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumeroMotor",
                table: "Automoviles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroChasis",
                table: "Automoviles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_NumeroChasis",
                table: "Automoviles",
                column: "NumeroChasis",
                unique: true,
                filter: "[NumeroChasis] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_NumeroMotor",
                table: "Automoviles",
                column: "NumeroMotor",
                unique: true,
                filter: "[NumeroMotor] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Automoviles_NumeroChasis",
                table: "Automoviles");

            migrationBuilder.DropIndex(
                name: "IX_Automoviles_NumeroMotor",
                table: "Automoviles");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroMotor",
                table: "Automoviles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroChasis",
                table: "Automoviles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
