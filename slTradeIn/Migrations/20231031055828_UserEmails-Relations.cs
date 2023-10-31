using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace slTradeIn.Migrations
{
    /// <inheritdoc />
    public partial class UserEmailsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UseriUserID",
                table: "Detail_TTU_userEmail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iUserID",
                table: "Detail_TTU_userEmail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Detail_TTU_userEmail_UseriUserID",
                table: "Detail_TTU_userEmail",
                column: "UseriUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_TTU_userEmail_Detail_TTU_user_UseriUserID",
                table: "Detail_TTU_userEmail",
                column: "UseriUserID",
                principalTable: "Detail_TTU_user",
                principalColumn: "iUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_TTU_userEmail_Detail_TTU_user_UseriUserID",
                table: "Detail_TTU_userEmail");

            migrationBuilder.DropIndex(
                name: "IX_Detail_TTU_userEmail_UseriUserID",
                table: "Detail_TTU_userEmail");

            migrationBuilder.DropColumn(
                name: "UseriUserID",
                table: "Detail_TTU_userEmail");

            migrationBuilder.DropColumn(
                name: "iUserID",
                table: "Detail_TTU_userEmail");
        }
    }
}
