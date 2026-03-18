using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityInternshipPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInternshipApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Internships_InternshipId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "InternshipApplication");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_StudentId",
                table: "InternshipApplication",
                newName: "IX_InternshipApplication_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_InternshipId",
                table: "InternshipApplication",
                newName: "IX_InternshipApplication_InternshipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipApplication",
                table: "InternshipApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipApplication_Internships_InternshipId",
                table: "InternshipApplication",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipApplication_Students_StudentId",
                table: "InternshipApplication",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipApplication_Internships_InternshipId",
                table: "InternshipApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipApplication_Students_StudentId",
                table: "InternshipApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipApplication",
                table: "InternshipApplication");

            migrationBuilder.RenameTable(
                name: "InternshipApplication",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipApplication_StudentId",
                table: "Applications",
                newName: "IX_Applications_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipApplication_InternshipId",
                table: "Applications",
                newName: "IX_Applications_InternshipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Internships_InternshipId",
                table: "Applications",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
