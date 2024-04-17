using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedSystems.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContextMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Patients_VaccinationCenter",
                table: "Vaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccinationCenters_PatientId",
                table: "Vaccines");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_Patients_PatientId",
                table: "Vaccines",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VaccinationCenters_VaccinationCenter",
                table: "Vaccines",
                column: "VaccinationCenter",
                principalTable: "VaccinationCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Patients_PatientId",
                table: "Vaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccinationCenters_VaccinationCenter",
                table: "Vaccines");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_Patients_VaccinationCenter",
                table: "Vaccines",
                column: "VaccinationCenter",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VaccinationCenters_PatientId",
                table: "Vaccines",
                column: "PatientId",
                principalTable: "VaccinationCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
