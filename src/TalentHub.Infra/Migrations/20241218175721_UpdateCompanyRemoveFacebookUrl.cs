using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentHub.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompanyRemoveFacebookUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "facebook_url",
                table: "companies");

            migrationBuilder.RenameColumn(
                name: "linkedin_url",
                table: "candidates",
                newName: "linked_in_url");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 908, DateTimeKind.Utc).AddTicks(6492),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(3102));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 908, DateTimeKind.Utc).AddTicks(6199),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(2202));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 889, DateTimeKind.Utc).AddTicks(9831),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(6517));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 889, DateTimeKind.Utc).AddTicks(9593),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(5856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 882, DateTimeKind.Utc).AddTicks(4554),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1997));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 882, DateTimeKind.Utc).AddTicks(4270),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1423));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "linked_in_url",
                table: "candidates",
                newName: "linkedin_url");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(3102),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 908, DateTimeKind.Utc).AddTicks(6492));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 669, DateTimeKind.Utc).AddTicks(2202),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 908, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(6517),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 889, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 613, DateTimeKind.Utc).AddTicks(5856),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 889, DateTimeKind.Utc).AddTicks(9593));

            migrationBuilder.AddColumn<string>(
                name: "facebook_url",
                table: "companies",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1997),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 882, DateTimeKind.Utc).AddTicks(4554));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 12, 54, 6, 596, DateTimeKind.Utc).AddTicks(1423),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 18, 17, 57, 20, 882, DateTimeKind.Utc).AddTicks(4270));
        }
    }
}
