using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cancer_system.Migrations
{
    /// <inheritdoc />
    public partial class AddYearsOfExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "years_of_experience",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "years_of_experience",
                table: "Doctors");
        }
    }
}
