using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class changeForClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVCollection_User_userID",
                table: "CVCollection");

            migrationBuilder.DropIndex(
                name: "IX_CVCollection_userID",
                table: "CVCollection");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "CVCollection");

            migrationBuilder.AlterColumn<string>(
                name: "EducationQualification",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CVCollectionID",
                table: "CV",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemote",
                table: "CV",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CV_CVCollectionID",
                table: "CV",
                column: "CVCollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_CV_CVCollection_CVCollectionID",
                table: "CV",
                column: "CVCollectionID",
                principalTable: "CVCollection",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CV_CVCollection_CVCollectionID",
                table: "CV");

            migrationBuilder.DropIndex(
                name: "IX_CV_CVCollectionID",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "CVCollectionID",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "IsRemote",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "CV");

            migrationBuilder.AddColumn<int>(
                name: "userID",
                table: "CVCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EducationQualification",
                table: "CV",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CVCollection_userID",
                table: "CVCollection",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_CVCollection_User_userID",
                table: "CVCollection",
                column: "userID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
