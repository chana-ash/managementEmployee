using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_vacations_UserId",
                table: "vacations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_vacations_users_UserId",
                table: "vacations",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vacations_users_UserId",
                table: "vacations");

            migrationBuilder.DropIndex(
                name: "IX_vacations_UserId",
                table: "vacations");
        }
    }
}
