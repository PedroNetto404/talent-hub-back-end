using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentHub.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8791),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 252, DateTimeKind.Utc).AddTicks(3046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8463),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 252, DateTimeKind.Utc).AddTicks(2285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(4115),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 220, DateTimeKind.Utc).AddTicks(1284));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(3760),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 220, DateTimeKind.Utc).AddTicks(788));

            migrationBuilder.AddColumn<bool>(
                name: "auto_match_enabled",
                table: "candidates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    legal_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    trade_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    recruitment_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    auto_match_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    employee_count = table.Column<int>(type: "integer", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    site_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    address_street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    address_neighborhood = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_state = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address_country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address_zip_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    about = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    instagram_url = table.Column<string>(type: "text", nullable: true),
                    facebook_url = table.Column<string>(type: "text", nullable: true),
                    linkedin_url = table.Column<string>(type: "text", nullable: true),
                    career_page_url = table.Column<string>(type: "text", nullable: true),
                    presentation_video_url = table.Column<string>(type: "text", nullable: true),
                    mission = table.Column<string>(type: "text", nullable: true),
                    vision = table.Column<string>(type: "text", nullable: true),
                    values = table.Column<string>(type: "text", nullable: true),
                    foundantion_year = table.Column<int>(type: "integer", nullable: false),
                    galery = table.Column<List<string>>(type: "text[]", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 754, DateTimeKind.Utc).AddTicks(1843)),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 754, DateTimeKind.Utc).AddTicks(2191)),
                    deleted_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_companies_cnpj",
                table: "companies",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_companies_phone",
                table: "companies",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_companies_recruitment_email",
                table: "companies",
                column: "recruitment_email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropColumn(
                name: "auto_match_enabled",
                table: "candidates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 252, DateTimeKind.Utc).AddTicks(3046),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 252, DateTimeKind.Utc).AddTicks(2285),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 771, DateTimeKind.Utc).AddTicks(8463));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 220, DateTimeKind.Utc).AddTicks(1284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(4115));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "candidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 3, 45, 37, 220, DateTimeKind.Utc).AddTicks(788),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 2, 12, 56, 27, 746, DateTimeKind.Utc).AddTicks(3760));
        }
    }
}
