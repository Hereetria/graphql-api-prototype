using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQL_Test_Project.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Courses_CourseDTOId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "CourseDTOId",
                table: "Student",
                newName: "CourseTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_CourseDTOId",
                table: "Student",
                newName: "IX_Student_CourseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Courses_CourseTypeId",
                table: "Student",
                column: "CourseTypeId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Courses_CourseTypeId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "CourseTypeId",
                table: "Student",
                newName: "CourseDTOId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_CourseTypeId",
                table: "Student",
                newName: "IX_Student_CourseDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Courses_CourseDTOId",
                table: "Student",
                column: "CourseDTOId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
