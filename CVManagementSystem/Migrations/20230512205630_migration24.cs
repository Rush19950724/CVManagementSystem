using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class migration24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVCollectionID",
                table: "CV",
                type: "int",
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
    }
}
