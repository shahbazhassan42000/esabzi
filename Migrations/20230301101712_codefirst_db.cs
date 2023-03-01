using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esabzi.Migrations
{
    /// <inheritdoc />
    public partial class codefirstdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://i.ibb.co/cT5mM2Z/profile-img.png"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "CUSTOMER"),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "ContactNo", "CreatedByUserId", "CreatedDate", "Email", "IsActive", "LastModifiedDate", "LastModifiedUserId", "Name", "Password", "Picture", "Role", "Username" },
                values: new object[] { 1, "Street #3, House #22, near Data Darbar, Lahore", "+923354058294", "2fd28110-93d0-427d-9207-d55dbca680fa", new DateTime(2023, 3, 1, 15, 17, 12, 300, DateTimeKind.Local).AddTicks(7890), "shahbazhassan42000@gmail.com", true, null, null, "Shahbaz", "$2a$11$DcSDoIIiZ9TFxcnUurrBg.UGa0SJGdBCxLIJE.GLcxg5hMIirYZdq", "https://i.ibb.co/HYJWqBc/Whats-App-Image-2022-10-19-at-23-57-52.jpg", "ADMIN", "shahbaz" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
