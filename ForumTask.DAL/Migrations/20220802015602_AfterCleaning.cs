using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumTask.DAL.Migrations
{
    public partial class AfterCleaning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_UserId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Messages_MessageId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Topics_TopicId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_CreatorId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marks",
                table: "Marks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "Marks",
                newName: "Mark");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_CreatorId",
                table: "Topic",
                newName: "IX_Topic_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_TopicId",
                table: "Message",
                newName: "IX_Message_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_AuthorId",
                table: "Message",
                newName: "IX_Message_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_MessageId",
                table: "Mark",
                newName: "IX_Mark_MessageId");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AspNetRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Topic",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Message",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Message",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "Type",
                table: "Mark",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Mark",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mark",
                table: "Mark",
                columns: new[] { "UserId", "MessageId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, "6d4fa982-7b49-4266-9922-01d74e265bdf", "User", "USER" },
                    { 2L, "308c3771-d582-45f6-9405-5b20bb86f531", "Moderator", "MODERATOR" },
                    { 3L, "42c605cf-4afb-453e-930c-21a5950b264c", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisterDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "7abb7240-5862-4256-a445-e771e6fead12", "admin@forum.here", true, false, false, null, "ADMIN@FORUM.HERE", "ADMIN", "AQAAAAEAACcQAAAAECdwFtp7HA4VBYXGAAZMRI39wfZHz1c8O5m/YRW2C6XR8NIqNQ+ssC+Mod4LmoEN7w==", null, false, new DateTime(2022, 8, 2, 1, 56, 1, 822, DateTimeKind.Utc).AddTicks(1378), "67d458dd-87df-4a0c-9813-430d84db2295", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 3L, 1L });

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_AspNetUsers_UserId",
                table: "Mark",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Message_MessageId",
                table: "Mark",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_AuthorId",
                table: "Message",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Topic_TopicId",
                table: "Message",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_AspNetUsers_CreatorId",
                table: "Topic",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_AspNetUsers_UserId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Message_MessageId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_AuthorId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Topic_TopicId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_AspNetUsers_CreatorId",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mark",
                table: "Mark");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3L, 1L });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "Mark",
                newName: "Marks");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_CreatorId",
                table: "Topics",
                newName: "IX_Topics_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_TopicId",
                table: "Messages",
                newName: "IX_Messages_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_AuthorId",
                table: "Messages",
                newName: "IX_Messages_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_MessageId",
                table: "Marks",
                newName: "IX_Marks_MessageId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Topics",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Messages",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Marks",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Marks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marks",
                table: "Marks",
                columns: new[] { "UserId", "MessageId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "c669c5da-cfac-4dfe-aebf-b5886e9aee41", "User", "USER" },
                    { 2, "e4b9554c-01ae-4441-b63a-cd3c84bc9b53", "Moderator", "MODERATOR" },
                    { 3, "bef98d41-ec24-4a53-a1a8-317977d999cf", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisterDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "9f2a478a-331b-41e8-9bb6-dbbe2fa7156d", "admin@forum.here", true, false, false, null, "ADMIN@FORUM.HERE", "ADMIN", "AQAAAAEAACcQAAAAELtOh/B3K1rVD+YCK/htIC+4iL3/6L72Wz+Ivjn+zEpbV78hgZFCPaE752Am3GXw2w==", null, false, new DateTime(2021, 10, 18, 15, 21, 31, 792, DateTimeKind.Utc).AddTicks(5502), "26e23017-2cac-49b9-9b3f-17d2afeefcf9", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 3, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_UserId",
                table: "Marks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Messages_MessageId",
                table: "Marks",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Topics_TopicId",
                table: "Messages",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_CreatorId",
                table: "Topics",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
