using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "id",
                schema: "led",
                table: "led_strip_type",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<short>(
                name: "led_strip_type_id",
                schema: "led",
                table: "led_strip",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "serial_number",
                schema: "led",
                table: "device",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "led",
                table: "device",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ip_address",
                schema: "led",
                table: "device",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "led",
                table: "device",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "schedule_type",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "schedule",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    scene_id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    schedule_type_id = table.Column<short>(type: "smallint", nullable: false),
                    run_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cron_expression = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_schedule_scene_scene_id",
                        column: x => x.scene_id,
                        principalSchema: "led",
                        principalTable: "scene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedule_schedule_type_schedule_type_id",
                        column: x => x.schedule_type_id,
                        principalSchema: "led",
                        principalTable: "schedule_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedule_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "led",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "led",
                table: "schedule_type",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { (short)1, "One Off" },
                    { (short)2, "Recurring" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_schedule_scene_id",
                schema: "led",
                table: "schedule",
                column: "scene_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_schedule_type_id",
                schema: "led",
                table: "schedule",
                column: "schedule_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_tenant_id",
                schema: "led",
                table: "schedule",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedule",
                schema: "led");

            migrationBuilder.DropTable(
                name: "schedule_type",
                schema: "led");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "led",
                table: "led_strip_type",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "led_strip_type_id",
                schema: "led",
                table: "led_strip",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "serial_number",
                schema: "led",
                table: "device",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "led",
                table: "device",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ip_address",
                schema: "led",
                table: "device",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "led",
                table: "device",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);
        }
    }
}
