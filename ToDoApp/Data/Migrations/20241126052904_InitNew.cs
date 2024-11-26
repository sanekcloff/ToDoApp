using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_AssigneeId",
                table: "Objectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_CreatorId",
                table: "Objectives");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_AssigneeId",
                table: "Objectives",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_CreatorId",
                table: "Objectives",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_AssigneeId",
                table: "Objectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_CreatorId",
                table: "Objectives");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_AssigneeId",
                table: "Objectives",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_CreatorId",
                table: "Objectives",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
