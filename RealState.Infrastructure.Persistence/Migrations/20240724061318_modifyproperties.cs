using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifyproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Upgrades_UpgradeId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UpgradeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UpgradeId",
                table: "Properties");

            migrationBuilder.CreateTable(
                name: "PropertiesUpgrades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpgradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesUpgrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertiesUpgrades_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesUpgrades_Upgrades_UpgradeId",
                        column: x => x.UpgradeId,
                        principalTable: "Upgrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesUpgrades_PropertyId",
                table: "PropertiesUpgrades",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesUpgrades_UpgradeId",
                table: "PropertiesUpgrades",
                column: "UpgradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertiesUpgrades");

            migrationBuilder.AddColumn<Guid>(
                name: "UpgradeId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UpgradeId",
                table: "Properties",
                column: "UpgradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Upgrades_UpgradeId",
                table: "Properties",
                column: "UpgradeId",
                principalTable: "Upgrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
