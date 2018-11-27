using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PatientChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patients",
                newName: "StatusCode");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Patients",
                newName: "SocialDistrictText");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilRegistrationNumber",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilStatusCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryIdentificationCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryIdentificationCodeSst",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryIdentificationText",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParishDistrictCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParishDistrictText",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonGenderCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonNameid",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationDistrictCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationDistrictText",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PractitionerIdentificationCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionalCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionalName",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialDistrictCode",
                table: "Patients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    HouseLetter = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    PostCodeIdentifier = table.Column<string>(nullable: true),
                    SideOrDoor = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    MunicipalityCode = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonName",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonName", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressId",
                table: "Patients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonNameid",
                table: "Patients",
                column: "PersonNameid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Address_AddressId",
                table: "Patients",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PersonName_PersonNameid",
                table: "Patients",
                column: "PersonNameid",
                principalTable: "PersonName",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Address_AddressId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PersonName_PersonNameid",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "PersonName");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AddressId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PersonNameid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CivilRegistrationNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CivilStatusCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryIdentificationCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryIdentificationCodeSst",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryIdentificationText",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ParishDistrictCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ParishDistrictText",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PersonGenderCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PersonNameid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PopulationDistrictCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PopulationDistrictText",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PractitionerIdentificationCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RegionalCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RegionalName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SocialDistrictCode",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "Patients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SocialDistrictText",
                table: "Patients",
                newName: "Address");
        }
    }
}
