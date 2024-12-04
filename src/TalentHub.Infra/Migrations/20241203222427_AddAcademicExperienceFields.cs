using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentHub.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicExperienceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 710, DateTimeKind.Utc).AddTicks(6413),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 710, DateTimeKind.Utc).AddTicks(5660),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8463));

            migrationBuilder.AddColumn<int>(
                name: "expected_graduation_month",
                table: "experiences",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "expected_graduation_year",
                table: "experiences",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 672, DateTimeKind.Utc).AddTicks(4463),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 754, DateTimeKind.Utc).AddTicks(2191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 672, DateTimeKind.Utc).AddTicks(3936),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 754, DateTimeKind.Utc).AddTicks(1843));

            migrationBuilder.AddColumn<Guid>(
                name: "sector_id",
                table: "companies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 656, DateTimeKind.Utc).AddTicks(1457),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(4115));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 656, DateTimeKind.Utc).AddTicks(907),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(3760));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expected_graduation_month",
                table: "experiences");

            migrationBuilder.DropColumn(
                name: "expected_graduation_year",
                table: "experiences");

            migrationBuilder.DropColumn(
                name: "sector_id",
                table: "companies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8791),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 710, DateTimeKind.Utc).AddTicks(6413));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8463),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 710, DateTimeKind.Utc).AddTicks(5660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 754, DateTimeKind.Utc).AddTicks(2191),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 672, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 754, DateTimeKind.Utc).AddTicks(1843),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 672, DateTimeKind.Utc).AddTicks(3936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(4115),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 656, DateTimeKind.Utc).AddTicks(1457));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(3760),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 24, 26, 656, DateTimeKind.Utc).AddTicks(907));
        }
    }
}
