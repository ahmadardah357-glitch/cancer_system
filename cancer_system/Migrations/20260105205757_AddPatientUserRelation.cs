using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
 
    public partial class AddPatientUserRelation : Migration
    {
    
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UserId1",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId1",
                table: "Patients",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UserId1",
                table: "Patients",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
