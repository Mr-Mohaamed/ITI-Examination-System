using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamProject.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTopic_Courses_CourseId",
                table: "CoursesTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTopic_Topics_TopicId",
                table: "CoursesTopic");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTopic_Courses_CourseId",
                table: "CoursesTopic",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTopic_Topics_TopicId",
                table: "CoursesTopic",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTopic_Courses_CourseId",
                table: "CoursesTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTopic_Topics_TopicId",
                table: "CoursesTopic");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTopic_Courses_CourseId",
                table: "CoursesTopic",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTopic_Topics_TopicId",
                table: "CoursesTopic",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
