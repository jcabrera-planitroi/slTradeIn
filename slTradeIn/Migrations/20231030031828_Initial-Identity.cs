using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace slTradeIn.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityUserAuth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityUserAuth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detail_TTU_identityRoleClaim_Detail_TTU_identityRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Detail_TTU_identityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detail_TTU_identityUserClaim_Detail_TTU_identityUserAuth_UserId",
                        column: x => x.UserId,
                        principalTable: "Detail_TTU_identityUserAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Detail_TTU_identityUserLogin_Detail_TTU_identityUserAuth_UserId",
                        column: x => x.UserId,
                        principalTable: "Detail_TTU_identityUserAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Detail_TTU_identityUserRole_Detail_TTU_identityRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Detail_TTU_identityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detail_TTU_identityUserRole_Detail_TTU_identityUserAuth_UserId",
                        column: x => x.UserId,
                        principalTable: "Detail_TTU_identityUserAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_identityUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_identityUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Detail_TTU_identityUserToken_Detail_TTU_identityUserAuth_UserId",
                        column: x => x.UserId,
                        principalTable: "Detail_TTU_identityUserAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detail_TTU_identityRoleClaim_RoleId",
                table: "Detail_TTU_identityRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Detail_TTU_identityRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Detail_TTU_identityUserAuth",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Detail_TTU_identityUserAuth",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_TTU_identityUserClaim_UserId",
                table: "Detail_TTU_identityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_TTU_identityUserLogin_UserId",
                table: "Detail_TTU_identityUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_TTU_identityUserRole_RoleId",
                table: "Detail_TTU_identityUserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detail_TTU_identityRoleClaim");

            migrationBuilder.DropTable(
                name: "Detail_TTU_identityUserClaim");

            migrationBuilder.DropTable(
                name: "Detail_TTU_identityUserLogin");

            migrationBuilder.DropTable(
                name: "Detail_TTU_identityUserRole");

            migrationBuilder.DropTable(
                name: "Detail_TTU_identityUserToken");

            migrationBuilder.DropTable(
                name: "Detail_TTU_identityRoles");

            migrationBuilder.DropTable(
                name: "Detail_TTU_identityUserAuth");
        }
    }
}
