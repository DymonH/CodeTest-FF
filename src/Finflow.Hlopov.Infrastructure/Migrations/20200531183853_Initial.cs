using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finflow.Hlopov.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "finflow_client_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Surname = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Remittance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    SendAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ReceiveAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    SendCurrencyId = table.Column<int>(nullable: false),
                    ReceiveCurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remittance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remittance_Currency_ReceiveCurrencyId",
                        column: x => x.ReceiveCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remittance_Client_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remittance_Currency_SendCurrencyId",
                        column: x => x.SendCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Remittance_Client_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemittanceStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceStatus_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceStatuses",
                columns: table => new
                {
                    RemittanceId = table.Column<Guid>(nullable: false),
                    RemittanceStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemittanceStatuses", x => new { x.RemittanceId, x.RemittanceStatusId });
                    table.ForeignKey(
                        name: "FK_RemittanceStatuses_Remittance_RemittanceId",
                        column: x => x.RemittanceId,
                        principalTable: "Remittance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemittanceStatuses_RemittanceStatus_RemittanceStatusId",
                        column: x => x.RemittanceStatusId,
                        principalTable: "RemittanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Name",
                table: "Client",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Surname",
                table: "Client",
                column: "Surname");

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_ReceiveCurrencyId",
                table: "Remittance",
                column: "ReceiveCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_ReceiverId",
                table: "Remittance",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_SendCurrencyId",
                table: "Remittance",
                column: "SendCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_SenderId",
                table: "Remittance",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceStatus_StatusId",
                table: "RemittanceStatus",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceStatuses_RemittanceStatusId",
                table: "RemittanceStatuses",
                column: "RemittanceStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemittanceStatuses");

            migrationBuilder.DropTable(
                name: "Remittance");

            migrationBuilder.DropTable(
                name: "RemittanceStatus");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropSequence(
                name: "finflow_client_hilo");
        }
    }
}
