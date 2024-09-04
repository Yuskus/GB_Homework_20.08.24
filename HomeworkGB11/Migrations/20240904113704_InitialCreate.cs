using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeworkGB11.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees_positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    base_salary = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("emp_position_id_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_zones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workzone_id_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees_list",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    hiring_date = table.Column<DateOnly>(type: "date", nullable: false),
                    home_adress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    workzone_id = table.Column<int>(type: "integer", nullable: true),
                    emp_position_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_id_pk", x => x.id);
                    table.ForeignKey(
                        name: "position_to_emp_id_fk",
                        column: x => x.emp_position_id,
                        principalTable: "employees_positions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "workzone_to_emp_id_fk",
                        column: x => x.workzone_id,
                        principalTable: "work_zones",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_list_emp_position_id",
                table: "employees_list",
                column: "emp_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_list_workzone_id",
                table: "employees_list",
                column: "workzone_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees_list");

            migrationBuilder.DropTable(
                name: "employees_positions");

            migrationBuilder.DropTable(
                name: "work_zones");
        }
    }
}
