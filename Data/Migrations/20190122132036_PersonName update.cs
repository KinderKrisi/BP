using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PersonNameupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PersonName_PersonNameid",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonName",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonNameid",
                table: "Patients",
                newName: "PatientNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_PersonNameid",
                table: "Patients",
                newName: "IX_Patients_PatientNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PersonName_PatientNameId",
                table: "Patients",
                column: "PatientNameId",
                principalTable: "PersonName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PersonName_PatientNameId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PersonName",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PatientNameId",
                table: "Patients",
                newName: "PersonNameid");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_PatientNameId",
                table: "Patients",
                newName: "IX_Patients_PersonNameid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PersonName_PersonNameid",
                table: "Patients",
                column: "PersonNameid",
                principalTable: "PersonName",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
