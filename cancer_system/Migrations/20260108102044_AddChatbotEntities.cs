using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddChatbotEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatbotSessions_Patients_patient_id",
                table: "ChatbotSessions");

            migrationBuilder.DropColumn(
                name: "reply",
                table: "ChatbotSessions");

            migrationBuilder.DropColumn(
                name: "session_type",
                table: "ChatbotSessions");

            migrationBuilder.RenameColumn(
                name: "patient_id",
                table: "ChatbotSessions",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "session_id",
                table: "ChatbotSessions",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "session_date",
                table: "ChatbotSessions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "question_text",
                table: "ChatbotSessions",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_ChatbotSessions_patient_id",
                table: "ChatbotSessions",
                newName: "IX_ChatbotSessions_PatientId");

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "ChatbotSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ChatbotMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatbotMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_ChatbotMessages_ChatbotSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ChatbotSessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotMessages_SessionId",
                table: "ChatbotMessages",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatbotSessions_Patients_PatientId",
                table: "ChatbotSessions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatbotSessions_Patients_PatientId",
                table: "ChatbotSessions");

            migrationBuilder.DropTable(
                name: "ChatbotMessages");

            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "ChatbotSessions");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "ChatbotSessions",
                newName: "patient_id");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "ChatbotSessions",
                newName: "session_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ChatbotSessions",
                newName: "question_text");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ChatbotSessions",
                newName: "session_date");

            migrationBuilder.RenameIndex(
                name: "IX_ChatbotSessions_PatientId",
                table: "ChatbotSessions",
                newName: "IX_ChatbotSessions_patient_id");

            migrationBuilder.AddColumn<string>(
                name: "reply",
                table: "ChatbotSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "session_type",
                table: "ChatbotSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatbotSessions_Patients_patient_id",
                table: "ChatbotSessions",
                column: "patient_id",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
