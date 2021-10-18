using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumTask.DAL.Migrations
{
    public partial class RelationFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c669c5da-cfac-4dfe-aebf-b5886e9aee41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e4b9554c-01ae-4441-b63a-cd3c84bc9b53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bef98d41-ec24-4a53-a1a8-317977d999cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate", "SecurityStamp" },
                values: new object[] { "9f2a478a-331b-41e8-9bb6-dbbe2fa7156d", "AQAAAAEAACcQAAAAELtOh/B3K1rVD+YCK/htIC+4iL3/6L72Wz+Ivjn+zEpbV78hgZFCPaE752Am3GXw2w==", new DateTime(2021, 10, 18, 15, 21, 31, 792, DateTimeKind.Utc).AddTicks(5502), "26e23017-2cac-49b9-9b3f-17d2afeefcf9" });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fe2e492a-54c0-487a-9a02-f6273b3d9bd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a3da107d-b316-4fdd-a8a4-d67bb9ef2a17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5278b026-796f-4e7c-8d32-f7f6f5b82c93");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate", "SecurityStamp" },
                values: new object[] { "107c6c51-9a40-40ca-85cd-56bbe6b01473", "AQAAAAEAACcQAAAAEJF5xL+Xuhxtc3Mn/TxzHvioN1JdVTcS0W5gWdvxeHNOu60LKhg+HIHjC8G6Rb5eVA==", new DateTime(2021, 10, 16, 23, 15, 36, 898, DateTimeKind.Utc).AddTicks(4257), "e2105f26-0693-4349-b657-806da1f1ef62" });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics",
                column: "CreatorId",
                unique: true,
                filter: "[CreatorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId",
                unique: true,
                filter: "[AuthorId] IS NOT NULL");
        }
    }
}
