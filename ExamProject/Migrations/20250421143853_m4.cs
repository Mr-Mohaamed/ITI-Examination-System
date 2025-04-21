using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamProject.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentResult",
                table: "Exams",
                newName: "TotalPoints");

            migrationBuilder.AddColumn<int>(
                name: "StudentPoints",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentPoints",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "TotalPoints",
                table: "Exams",
                newName: "StudentResult");
        }
    }
}
