using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumTask.DAL.Migrations
{
    public partial class IdentityInitValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, "c3c1d250-4176-4028-8a97-f44898497a9b", "User", "USER" },
                    { 2L, "d0d5d46d-34c3-40fd-b7e3-88ff88d36549", "Moderator", "MODERATOR" },
                    { 3L, "fe735399-480b-4577-94ed-12b1620ddc6e", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisterDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "222b7ec7-b5f6-43ed-b9e3-2179458c1169", "admin@forum.here", true, false, false, null, "ADMIN@FORUM.HERE", "ADMIN", "AQAAAAEAACcQAAAAEPKwgpDIig+szx2VtSMZroJqGZsyhSZG7jeopsulh5HJAwXVRjHCGSTezkHNjDgQTw==", null, false, new DateTime(2021, 10, 9, 20, 6, 21, 671, DateTimeKind.Utc).AddTicks(1047), null, false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
