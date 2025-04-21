using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamProject.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoursesTopic");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseAssignments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BranchTracks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoursesTopic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BranchTracks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
