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
                name: "VocationCredential",
                table: "Instructors",
                newName: "VocationCredential");

            migrationBuilder.RenameColumn(
                name: "Mood",
                table: "Instructors",
                newName: "Mood");
        }
    }
}
