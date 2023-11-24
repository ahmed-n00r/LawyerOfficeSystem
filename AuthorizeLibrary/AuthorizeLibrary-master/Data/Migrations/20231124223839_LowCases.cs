using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermissionBasedAuthorization.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class LowCases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LowCases",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Case = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimantId = table.Column<long>(type: "bigint", nullable: false),
                    LoweyrId = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnterdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnterBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowCases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LowCases_Claimants_ClaimantId",
                        column: x => x.ClaimantId,
                        principalTable: "Claimants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LowCases_lowyers_LoweyrId",
                        column: x => x.LoweyrId,
                        principalTable: "lowyers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LowCases_ClaimantId",
                table: "LowCases",
                column: "ClaimantId");

            migrationBuilder.CreateIndex(
                name: "IX_LowCases_LoweyrId",
                table: "LowCases",
                column: "LoweyrId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LowCases");
        }
    }
}
