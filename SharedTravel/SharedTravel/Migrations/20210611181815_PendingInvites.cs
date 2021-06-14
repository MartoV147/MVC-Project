using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedTravel.Migrations
{
    public partial class PendingInvites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PendingInvites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TravelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingInvites", x => new { x.UserId, x.TravelId });
                    table.ForeignKey(
                        name: "FK_PendingInvites_Travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "Travels",
                        principalColumn: "TravelId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PendingInvites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "TravelId",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2021, 6, 11, 21, 18, 14, 553, DateTimeKind.Local).AddTicks(2869), new DateTime(2021, 6, 11, 21, 18, 14, 550, DateTimeKind.Local).AddTicks(8811) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "gladitorianpass");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "nikipass");

            migrationBuilder.CreateIndex(
                name: "IX_PendingInvites_TravelId",
                table: "PendingInvites",
                column: "TravelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingInvites");

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "TravelId",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2021, 6, 7, 19, 56, 14, 338, DateTimeKind.Local).AddTicks(5906), new DateTime(2021, 6, 7, 19, 56, 14, 333, DateTimeKind.Local).AddTicks(2474) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: null);
        }
    }
}
