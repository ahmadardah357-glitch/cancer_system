using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientMedical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Targeted_Area",
                table: "Radiation_Therapys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Sessions_Count",
                table: "Radiation_Therapys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Radiation_Therapys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Radiation_Therapys_PatientId",
                table: "Radiation_Therapys",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Radiation_Therapys_Patients_PatientId",
                table: "Radiation_Therapys",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Radiation_Therapys_Patients_PatientId",
                table: "Radiation_Therapys");

            migrationBuilder.DropIndex(
                name: "IX_Radiation_Therapys_PatientId",
                table: "Radiation_Therapys");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Radiation_Therapys");

            migrationBuilder.AlterColumn<string>(
                name: "Targeted_Area",
                table: "Radiation_Therapys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Sessions_Count",
                table: "Radiation_Therapys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
