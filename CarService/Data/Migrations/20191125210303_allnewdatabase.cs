using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarService.Data.Migrations
{
    public partial class allnewdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorksheetId",
                table: "Reservations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worksheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<string>(nullable: true),
                    MechanicId = table.Column<string>(nullable: true),
                    ReservationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worksheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worksheets_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Worksheets_AspNetUsers_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostsToWorksheets",
                columns: table => new
                {
                    CostId = table.Column<int>(nullable: false),
                    WorksheetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostsToWorksheets", x => new { x.CostId, x.WorksheetId });
                    table.ForeignKey(
                        name: "FK_CostsToWorksheets_Costs_CostId",
                        column: x => x.CostId,
                        principalTable: "Costs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostsToWorksheets_Worksheets_WorksheetId",
                        column: x => x.WorksheetId,
                        principalTable: "Worksheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_WorksheetId",
                table: "Reservations",
                column: "WorksheetId",
                unique: true,
                filter: "[WorksheetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CostsToWorksheets_WorksheetId",
                table: "CostsToWorksheets",
                column: "WorksheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Worksheets_ClientId",
                table: "Worksheets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Worksheets_MechanicId",
                table: "Worksheets",
                column: "MechanicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Worksheets_WorksheetId",
                table: "Reservations",
                column: "WorksheetId",
                principalTable: "Worksheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Worksheets_WorksheetId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "CostsToWorksheets");

            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "Worksheets");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_WorksheetId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "WorksheetId",
                table: "Reservations");
        }
    }
}
