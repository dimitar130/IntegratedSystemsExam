using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedSystems.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Patients_PatientId",
                table: "Vaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccinationCenters_CenterId",
                table: "Vaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_CenterId",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_PatientId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "Vaccines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines",
                columns: new[] { "PatientId", "VaccinationCenter" });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_VaccinationCenter",
                table: "Vaccines",
                column: "VaccinationCenter");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Patients_VaccinationCenter",
                table: "Vaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccinationCenters_PatientId",
                table: "Vaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_VaccinationCenter",
                table: "Vaccines");

            migrationBuilder.AddColumn<Guid>(
                name: "CenterId",
                table: "Vaccines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_CenterId",
                table: "Vaccines",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_PatientId",
                table: "Vaccines",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_Patients_PatientId",
                table: "Vaccines",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VaccinationCenters_CenterId",
                table: "Vaccines",
                column: "CenterId",
                principalTable: "VaccinationCenters",
                principalColumn: "Id");
        }
    }
}
