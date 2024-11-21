using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentHub.Infra.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    summary = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    profile_picture_url = table.Column<string>(type: "text", nullable: true),
                    resume_url = table.Column<string>(type: "text", nullable: true),
                    instagram_url = table.Column<string>(type: "text", nullable: true),
                    linkedin_url = table.Column<string>(type: "text", nullable: true),
                    github_url = table.Column<string>(type: "text", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    expected_remuneration = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    address_street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    address_neighborhood = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_state = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address_country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address_zip_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    desired_job_types = table.Column<string[]>(type: "text[]", nullable: false),
                    desired_workplace_types = table.Column<string[]>(type: "text[]", nullable: false),
                    hobbies = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "educational_institute",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_educational_institute", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    approved = table.Column<bool>(type: "boolean", nullable: false),
                    tags = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skills", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "certificates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    institution = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    workload = table.Column<double>(type: "double precision", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    candidate_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_certificates", x => x.id);
                    table.ForeignKey(
                        name: "fk_certificates_candidates_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "candidates",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "experiences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_year = table.Column<int>(type: "integer", nullable: false),
                    start_month = table.Column<int>(type: "integer", nullable: false),
                    end_year = table.Column<int>(type: "integer", nullable: true),
                    end_month = table.Column<int>(type: "integer", nullable: true),
                    is_current = table.Column<bool>(type: "boolean", nullable: false),
                    candidate_id = table.Column<Guid>(type: "uuid", nullable: true),
                    activities = table.Column<List<string>>(type: "text[]", nullable: false),
                    experience_type = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    level = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    course_id = table.Column<Guid>(type: "uuid", nullable: true),
                    institution_id = table.Column<Guid>(type: "uuid", nullable: true),
                    position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    professional_experience_level = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_experiences", x => x.id);
                    table.ForeignKey(
                        name: "fk_experiences_candidates_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "candidates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_experiences_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_experiences_educational_institute_institution_id",
                        column: x => x.institution_id,
                        principalTable: "educational_institute",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidate_skills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_id = table.Column<Guid>(type: "uuid", nullable: false),
                    proficiency = table.Column<string>(type: "text", nullable: false),
                    candidate_id = table.Column<Guid>(type: "uuid", nullable: true),
                    skill_type = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    special_proficiences = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidate_skills", x => x.id);
                    table.ForeignKey(
                        name: "fk_candidate_skills_candidates_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "candidates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_candidate_skills_skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_candidate_skills_candidate_id",
                table: "candidate_skills",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "ix_candidate_skills_skill_id",
                table: "candidate_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "ix_candidates_email",
                table: "candidates",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_candidates_phone",
                table: "candidates",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_certificates_candidate_id",
                table: "certificates",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "ix_experiences_candidate_id",
                table: "experiences",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "ix_experiences_course_id",
                table: "experiences",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_experiences_institution_id",
                table: "experiences",
                column: "institution_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_skills");

            migrationBuilder.DropTable(
                name: "certificates");

            migrationBuilder.DropTable(
                name: "experiences");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "candidates");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "educational_institute");
        }
    }
}
