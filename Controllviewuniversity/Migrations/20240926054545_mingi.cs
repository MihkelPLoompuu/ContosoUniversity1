using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class mingi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Instructors",
                newName: "VocationCredential");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Instructors",
                newName: "Mood");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VocationCredential",
                table: "Instructors",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Mood",
                table: "Instructors",
                newName: "Age");
        }
    }
}
