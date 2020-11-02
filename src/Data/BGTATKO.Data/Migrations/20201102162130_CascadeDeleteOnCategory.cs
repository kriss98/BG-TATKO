using Microsoft.EntityFrameworkCore.Migrations;

namespace BGTATKO.Data.Migrations
{
    public partial class CascadeDeleteOnCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId1",
                table: "Posts",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId1",
                table: "Posts",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoryId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Posts");
        }
    }
}
