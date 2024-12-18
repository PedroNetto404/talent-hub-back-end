using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentHub.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UserProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(3102),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 698, DateTimeKind.Utc).AddTicks(8734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(2202),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 698, DateTimeKind.Utc).AddTicks(8396));

            migrationBuilder.AddColumn<string>(
                name: "profile_picture_url",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(6517),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 678, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(5856),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 678, DateTimeKind.Utc).AddTicks(6967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1997),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 670, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1423),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 670, DateTimeKind.Utc).AddTicks(3555));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_picture_url",
                table: "users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 698, DateTimeKind.Utc).AddTicks(8734),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(3102));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 698, DateTimeKind.Utc).AddTicks(8396),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(2202));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 678, DateTimeKind.Utc).AddTicks(7247),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(6517));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 678, DateTimeKind.Utc).AddTicks(6967),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(5856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 670, DateTimeKind.Utc).AddTicks(3850),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1997));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 15, 20, 50, 5, 670, DateTimeKind.Utc).AddTicks(3555),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1423));
        }
    }
}
