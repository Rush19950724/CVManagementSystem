using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class newchangestodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EducationLevel",
                table: "CV",
                newName: "SecondaryEducation");

            migrationBuilder.RenameColumn(
                name: "AddressLine3",
                table: "CV",
                newName: "PrimaryEducation");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "CV",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "CV",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience1",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience2",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience3",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HigherEducation",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience1",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "Experience2",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "Experience3",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "HigherEducation",
                table: "CV");

            migrationBuilder.RenameColumn(
                name: "SecondaryEducation",
                table: "CV",
                newName: "EducationLevel");

            migrationBuilder.RenameColumn(
                name: "PrimaryEducation",
                table: "CV",
                newName: "AddressLine3");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
