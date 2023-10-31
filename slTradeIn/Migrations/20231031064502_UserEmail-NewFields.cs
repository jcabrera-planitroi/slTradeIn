using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace slTradeIn.Migrations
{
    /// <inheritdoc />
    public partial class UserEmailNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "vEmailProvider",
                table: "Detail_TTU_userEmail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vEmailProvider",
                table: "Detail_TTU_userEmail");
        }
    }
}
