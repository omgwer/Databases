using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "progress",
                table: "course_status",
                type: "numeric(3)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,0)",
                oldPrecision: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "progress",
                table: "course_module_status",
                type: "numeric(3)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,0)",
                oldPrecision: 3,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "course",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "course");

            migrationBuilder.AlterColumn<decimal>(
                name: "progress",
                table: "course_status",
                type: "numeric(3,0)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3)",
                oldPrecision: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "progress",
                table: "course_module_status",
                type: "numeric(3,0)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3)",
                oldPrecision: 3,
                oldNullable: true);
        }
    }
}
