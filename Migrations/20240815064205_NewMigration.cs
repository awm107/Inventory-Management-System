using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 15, 11, 42, 5, 333, DateTimeKind.Local).AddTicks(203), new DateTime(2024, 8, 15, 11, 42, 5, 334, DateTimeKind.Local).AddTicks(9315) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 15, 11, 42, 5, 335, DateTimeKind.Local).AddTicks(312), new DateTime(2024, 8, 15, 11, 42, 5, 335, DateTimeKind.Local).AddTicks(336) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 15, 11, 42, 5, 335, DateTimeKind.Local).AddTicks(366), new DateTime(2024, 8, 15, 11, 42, 5, 335, DateTimeKind.Local).AddTicks(368) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 15, 9, 8, 48, 560, DateTimeKind.Local).AddTicks(415), new DateTime(2024, 8, 15, 9, 8, 48, 562, DateTimeKind.Local).AddTicks(7331) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 15, 9, 8, 48, 562, DateTimeKind.Local).AddTicks(8402), new DateTime(2024, 8, 15, 9, 8, 48, 562, DateTimeKind.Local).AddTicks(8434) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 15, 9, 8, 48, 562, DateTimeKind.Local).AddTicks(8466), new DateTime(2024, 8, 15, 9, 8, 48, 562, DateTimeKind.Local).AddTicks(8468) });
        }
    }
}
