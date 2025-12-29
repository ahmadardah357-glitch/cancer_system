using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddDiagnosisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosiss_Patients_patient_id",
                table: "Diagnosiss");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentPlans_Diagnosiss_diagnosis_id",
                table: "TreatmentPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnosiss",
                table: "Diagnosiss");

            migrationBuilder.RenameTable(
                name: "Diagnosiss",
                newName: "Diagnosis");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosiss_patient_id",
                table: "Diagnosis",
                newName: "IX_Diagnosis_patient_id");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "Diagnosis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "stage",
                table: "Diagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tumor_grade",
                table: "Diagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tumor_location",
                table: "Diagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis",
                column: "diagnosis_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_Patients_patient_id",
                table: "Diagnosis",
                column: "patient_id",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentPlans_Diagnosis_diagnosis_id",
                table: "TreatmentPlans",
                column: "diagnosis_id",
                principalTable: "Diagnosis",
                principalColumn: "diagnosis_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_Patients_patient_id",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentPlans_Diagnosis_diagnosis_id",
                table: "TreatmentPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "stage",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "tumor_grade",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "tumor_location",
                table: "Diagnosis");

            migrationBuilder.RenameTable(
                name: "Diagnosis",
                newName: "Diagnosiss");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_patient_id",
                table: "Diagnosiss",
                newName: "IX_Diagnosiss_patient_id");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "Diagnosiss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnosiss",
                table: "Diagnosiss",
                column: "diagnosis_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosiss_Patients_patient_id",
                table: "Diagnosiss",
                column: "patient_id",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentPlans_Diagnosiss_diagnosis_id",
                table: "TreatmentPlans",
                column: "diagnosis_id",
                principalTable: "Diagnosiss",
                principalColumn: "diagnosis_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
