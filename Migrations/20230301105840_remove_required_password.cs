using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esabzi.Migrations
{
    /// <inheritdoc />
    public partial class removerequiredpassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "CUSTOMER");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "https://i.ibb.co/cT5mM2Z/profile-img.png");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2023, 3, 1, 15, 58, 39, 271, DateTimeKind.Local).AddTicks(9999), "$2a$11$NR8ANtczTzlNrNn5QUH8EObF1eDBy2hI96YMQLHIpKcCGEQ2ktHCa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "CUSTOMER",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "https://i.ibb.co/cT5mM2Z/profile-img.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Password" },
                values: new object[] { new DateTime(2023, 3, 1, 15, 17, 12, 300, DateTimeKind.Local).AddTicks(7890), "$2a$11$DcSDoIIiZ9TFxcnUurrBg.UGa0SJGdBCxLIJE.GLcxg5hMIirYZdq" });
        }
    }
}
