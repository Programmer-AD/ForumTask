using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumTask.DAL.Migrations
{
    public partial class OnDelSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_CreatorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_CreatorId",
                table: "Topics",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_CreatorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8b47a6ee-4863-4f5f-842e-1686c0e815ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1904a5ec-3f48-49dd-a594-e946552d24e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "80eb377c-ffc5-4d80-a9d8-f158300b8bf6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate", "SecurityStamp" },
                values: new object[] { "b273f5e4-cab3-4fea-8a93-1d8e8e8c373a", "AQAAAAEAACcQAAAAEKcjxgSWHJ3w8jZP8uMrwno+Emzr7wcBBLIRv9vurOkeiyNsrvvPTN1asTHY4xD5VQ==", new DateTime(2021, 10, 15, 17, 47, 25, 22, DateTimeKind.Utc).AddTicks(9343), "60b23f1a-43e8-4892-ac97-cad234517a96" });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_CreatorId",
                table: "Topics",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
