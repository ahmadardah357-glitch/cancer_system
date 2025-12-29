using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeysToMedicalEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "surgerys",
                newName: "SurgeryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RadiologyImages",
                newName: "RadiologyImageId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Radiation_Therapys",
                newName: "RadiationTherapyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Liver_Functions",
                newName: "LiverFunctionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "kidney_Functions",
                newName: "KidneyFunctionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Chemotherapys",
                newName: "ChemotherapyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CBCs",
                newName: "CbcId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurgeryId",
                table: "surgerys",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RadiologyImageId",
                table: "RadiologyImages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RadiationTherapyId",
                table: "Radiation_Therapys",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LiverFunctionId",
                table: "Liver_Functions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "KidneyFunctionId",
                table: "kidney_Functions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ChemotherapyId",
                table: "Chemotherapys",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CbcId",
                table: "CBCs",
                newName: "Id");
        }
    }
}
