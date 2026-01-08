using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChatbotSessionAndMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatbotSessions_Patients_PatientId",
                table: "ChatbotSessions");

            migrationBuilder.DropIndex(
                name: "IX_ChatbotSessions_PatientId",
                table: "ChatbotSessions");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "ChatbotSessions",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ChatbotSessions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "ChatbotSessions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "ChatbotSessions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "ChatbotMessages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AnswerIndex",
                table: "ChatbotMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ChatbotSessions");

            migrationBuilder.DropColumn(
                name: "AnswerIndex",
                table: "ChatbotMessages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ChatbotSessions",
                newName: "PatientId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ChatbotSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "ChatbotSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "ChatbotMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotSessions_PatientId",
                table: "ChatbotSessions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatbotSessions_Patients_PatientId",
                table: "ChatbotSessions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
