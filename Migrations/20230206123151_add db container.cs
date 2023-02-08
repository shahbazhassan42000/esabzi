using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esabzi.Migrations
{
    /// <inheritdoc />
    public partial class adddbcontainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2023, 2, 6, 17, 31, 50, 901, DateTimeKind.Local).AddTicks(8548), "$2a$11$2kbBDJvsds0ZIx3YNjQeHO.faCzse4g7wxW7dIVwFsHm7VcpdM8ku" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2023, 2, 6, 0, 2, 20, 681, DateTimeKind.Local).AddTicks(363), "$2a$11$1MG/I98CXtKBV/.DbeDb9OYShhBmkBojdLA2DV5RnOl4sgz5weqza" });
        }
    }
}
