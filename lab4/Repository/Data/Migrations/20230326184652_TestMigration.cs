using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    course_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("course_pkey", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "course_status",
                columns: table => new
                {
                    enrollment_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    progress = table.Column<decimal>(type: "numeric(3)", precision: 3, nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("course_status_pkey", x => x.enrollment_id);
                });

            migrationBuilder.CreateTable(
                name: "course_module",
                columns: table => new
                {
                    module_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    course_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    is_required = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    create_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("course_module_pkey", x => x.module_id);
                    table.ForeignKey(
                        name: "course_module_course_id_fkey",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "course_id");
                });

            migrationBuilder.CreateTable(
                name: "course_enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    course_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("course_enrollment_pkey", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "course_enrollment_course_id_fkey",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "course_enrollment_enrollment_id_fkey",
                        column: x => x.enrollment_id,
                        principalTable: "course_status",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_module_status",
                columns: table => new
                {
                    enrollment_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    module_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    progress = table.Column<decimal>(type: "numeric(3)", precision: 3, nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("course_module_status_pkey", x => new { x.enrollment_id, x.module_id });
                    table.ForeignKey(
                        name: "course_module_status_enrollment_id_fkey",
                        column: x => x.enrollment_id,
                        principalTable: "course_status",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "course_module_status_module_id_fkey",
                        column: x => x.module_id,
                        principalTable: "course_module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_course_enrollment_course_id",
                table: "course_enrollment",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_module_course_id",
                table: "course_module",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_module_status_module_id",
                table: "course_module_status",
                column: "module_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_enrollment");

            migrationBuilder.DropTable(
                name: "course_module_status");

            migrationBuilder.DropTable(
                name: "course_status");

            migrationBuilder.DropTable(
                name: "course_module");

            migrationBuilder.DropTable(
                name: "course");
        }
    }
}
