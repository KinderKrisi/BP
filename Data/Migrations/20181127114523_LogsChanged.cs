using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class LogsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Logs");
        }
    }
}
