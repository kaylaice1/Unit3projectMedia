using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit3project.Migrations
{
    /// <inheritdoc />
    public partial class RemovePasswordColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CommentDate",
                value: new DateTime(2024, 11, 6, 17, 47, 39, 220, DateTimeKind.Local).AddTicks(7620));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostDate",
                value: new DateTime(2024, 11, 6, 17, 47, 39, 220, DateTimeKind.Local).AddTicks(7585));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 11, 6, 17, 47, 39, 220, DateTimeKind.Local).AddTicks(7382));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CommentDate",
                value: new DateTime(2024, 11, 6, 16, 47, 45, 77, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostDate",
                value: new DateTime(2024, 11, 6, 16, 47, 45, 77, DateTimeKind.Local).AddTicks(6385));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 11, 6, 16, 47, 45, 77, DateTimeKind.Local).AddTicks(6193));
        }
    }
}
