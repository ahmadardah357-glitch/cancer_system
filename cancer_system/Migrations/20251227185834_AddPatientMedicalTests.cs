using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientMedicalTests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Billrubin",
                table: "Liver_Functions");

            migrationBuilder.DropColumn(
                name: "diagnosis_data",
                table: "Cancer_informations");

            migrationBuilder.RenameColumn(
                name: "surgery_Report",
                table: "surgerys",
                newName: "Surgery_Report");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "surgerys",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tumor_location",
                table: "Cancer_informations",
                newName: "Tumor_Location");

            migrationBuilder.RenameColumn(
                name: "tumor_grade",
                table: "Cancer_informations",
                newName: "Tumor_Grade");

            migrationBuilder.RenameColumn(
                name: "stage",
                table: "Cancer_informations",
                newName: "Stage");

            migrationBuilder.AlterColumn<string>(
                name: "Surgery_Report",
                table: "surgerys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "surgerys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AlP",
                table: "Liver_Functions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AST",
                table: "Liver_Functions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ALT",
                table: "Liver_Functions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Bilirubin",
                table: "Liver_Functions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Liver_Functions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Creatinine",
                table: "kidney_Functions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "BUN",
                table: "kidney_Functions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "kidney_Functions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Drug_Name",
                table: "Chemotherapys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Dose",
                table: "Chemotherapys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Chemotherapys",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Chemotherapys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Tumor_Location",
                table: "Cancer_informations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Tumor_Grade",
                table: "Cancer_informations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Stage",
                table: "Cancer_informations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis_Date",
                table: "Cancer_informations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Cancer_informations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_surgerys_PatientId",
                table: "surgerys",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Liver_Functions_PatientId",
                table: "Liver_Functions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_kidney_Functions_PatientId",
                table: "kidney_Functions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Chemotherapys_PatientId",
                table: "Chemotherapys",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cancer_informations_PatientId",
                table: "Cancer_informations",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancer_informations_Patients_PatientId",
                table: "Cancer_informations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chemotherapys_Patients_PatientId",
                table: "Chemotherapys",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_kidney_Functions_Patients_PatientId",
                table: "kidney_Functions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Liver_Functions_Patients_PatientId",
                table: "Liver_Functions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_surgerys_Patients_PatientId",
                table: "surgerys",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancer_informations_Patients_PatientId",
                table: "Cancer_informations");

            migrationBuilder.DropForeignKey(
                name: "FK_Chemotherapys_Patients_PatientId",
                table: "Chemotherapys");

            migrationBuilder.DropForeignKey(
                name: "FK_kidney_Functions_Patients_PatientId",
                table: "kidney_Functions");

            migrationBuilder.DropForeignKey(
                name: "FK_Liver_Functions_Patients_PatientId",
                table: "Liver_Functions");

            migrationBuilder.DropForeignKey(
                name: "FK_surgerys_Patients_PatientId",
                table: "surgerys");

            migrationBuilder.DropIndex(
                name: "IX_surgerys_PatientId",
                table: "surgerys");

            migrationBuilder.DropIndex(
                name: "IX_Liver_Functions_PatientId",
                table: "Liver_Functions");

            migrationBuilder.DropIndex(
                name: "IX_kidney_Functions_PatientId",
                table: "kidney_Functions");

            migrationBuilder.DropIndex(
                name: "IX_Chemotherapys_PatientId",
                table: "Chemotherapys");

            migrationBuilder.DropIndex(
                name: "IX_Cancer_informations_PatientId",
                table: "Cancer_informations");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "surgerys");

            migrationBuilder.DropColumn(
                name: "Bilirubin",
                table: "Liver_Functions");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Liver_Functions");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "kidney_Functions");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Chemotherapys");

            migrationBuilder.DropColumn(
                name: "Diagnosis_Date",
                table: "Cancer_informations");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Cancer_informations");

            migrationBuilder.RenameColumn(
                name: "Surgery_Report",
                table: "surgerys",
                newName: "surgery_Report");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "surgerys",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Tumor_Location",
                table: "Cancer_informations",
                newName: "tumor_location");

            migrationBuilder.RenameColumn(
                name: "Tumor_Grade",
                table: "Cancer_informations",
                newName: "tumor_grade");

            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "Cancer_informations",
                newName: "stage");

            migrationBuilder.AlterColumn<string>(
                name: "surgery_Report",
                table: "surgerys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AlP",
                table: "Liver_Functions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AST",
                table: "Liver_Functions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ALT",
                table: "Liver_Functions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Billrubin",
                table: "Liver_Functions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Creatinine",
                table: "kidney_Functions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BUN",
                table: "kidney_Functions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Drug_Name",
                table: "Chemotherapys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Dose",
                table: "Chemotherapys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Chemotherapys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "tumor_location",
                table: "Cancer_informations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "tumor_grade",
                table: "Cancer_informations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "stage",
                table: "Cancer_informations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "diagnosis_data",
                table: "Cancer_informations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
