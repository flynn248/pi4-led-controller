using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Updates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ip_address",
                schema: "led",
                table: "device",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "serial_number",
                schema: "led",
                table: "device",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "effect_type",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_builtin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_implmeneted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    schema_version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effect_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scene",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scene", x => x.id);
                    table.ForeignKey(
                        name: "FK_scene_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "led",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "effect",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    scene_id = table.Column<Guid>(type: "uuid", nullable: false),
                    led_strip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    effect_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    parameter_json_schema_version = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    parameter_json = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effect", x => x.id);
                    table.ForeignKey(
                        name: "FK_effect_effect_type_effect_type_id",
                        column: x => x.effect_type_id,
                        principalSchema: "led",
                        principalTable: "effect_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_effect_led_strip_led_strip_id",
                        column: x => x.led_strip_id,
                        principalSchema: "led",
                        principalTable: "led_strip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_effect_scene_scene_id",
                        column: x => x.scene_id,
                        principalSchema: "led",
                        principalTable: "scene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tenant_user_tenant_id",
                schema: "led",
                table: "tenant_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_device_serial_number",
                schema: "led",
                table: "device",
                column: "serial_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_effect_effect_type_id",
                schema: "led",
                table: "effect",
                column: "effect_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_effect_led_strip_id",
                schema: "led",
                table: "effect",
                column: "led_strip_id");

            migrationBuilder.CreateIndex(
                name: "IX_effect_scene_id_led_strip_id",
                schema: "led",
                table: "effect",
                columns: new[] { "scene_id", "led_strip_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_scene_tenant_id",
                schema: "led",
                table: "scene",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "effect",
                schema: "led");

            migrationBuilder.DropTable(
                name: "effect_type",
                schema: "led");

            migrationBuilder.DropTable(
                name: "scene",
                schema: "led");

            migrationBuilder.DropIndex(
                name: "IX_tenant_user_tenant_id",
                schema: "led",
                table: "tenant_user");

            migrationBuilder.DropIndex(
                name: "IX_device_serial_number",
                schema: "led",
                table: "device");

            migrationBuilder.DropColumn(
                name: "ip_address",
                schema: "led",
                table: "device");

            migrationBuilder.DropColumn(
                name: "serial_number",
                schema: "led",
                table: "device");
        }
    }
}
