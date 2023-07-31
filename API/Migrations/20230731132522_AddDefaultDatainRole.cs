using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AddDefaultDatainRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"), new DateTime(2023, 7, 31, 20, 25, 22, 2, DateTimeKind.Local).AddTicks(6735), new DateTime(2023, 7, 31, 20, 25, 22, 2, DateTimeKind.Local).AddTicks(6736), "Manager" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"), new DateTime(2023, 7, 31, 20, 25, 22, 2, DateTimeKind.Local).AddTicks(6715), new DateTime(2023, 7, 31, 20, 25, 22, 2, DateTimeKind.Local).AddTicks(6729), "Employee" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"), new DateTime(2023, 7, 31, 20, 25, 22, 2, DateTimeKind.Local).AddTicks(6791), new DateTime(2023, 7, 31, 20, 25, 22, 2, DateTimeKind.Local).AddTicks(6792), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"));
        }
    }
}
