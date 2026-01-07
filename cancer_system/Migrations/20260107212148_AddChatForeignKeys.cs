using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddChatForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "Chats",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "message_text",
                table: "Chats",
                newName: "MessageText");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                table: "Chats",
                newName: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_receiver_id",
                table: "Chats",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_sender_id",
                table: "Chats",
                column: "sender_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_receiver_id",
                table: "Chats",
                column: "receiver_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_sender_id",
                table: "Chats",
                column: "sender_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_receiver_id",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_sender_id",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_receiver_id",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_sender_id",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "Chats",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "MessageText",
                table: "Chats",
                newName: "message_text");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Chats",
                newName: "chat_id");
        }
    }
}
