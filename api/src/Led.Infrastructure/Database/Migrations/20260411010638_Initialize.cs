using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "led");

            migrationBuilder.CreateTable(
                name: "effect_type",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
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
                name: "led_strip_type",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_led_strip_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parameter_data_type",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parameter_data_type", x => x.id);
                });

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
                name: "tenant",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "effect_parameter_schema",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    effect_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    key = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    parameter_data_type_id = table.Column<short>(type: "smallint", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    min_value = table.Column<double>(type: "double precision", nullable: true),
                    max_value = table.Column<double>(type: "double precision", nullable: true),
                    allowed_values_json = table.Column<string>(type: "jsonb", maxLength: 1280, nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effect_parameter_schema", x => x.id);
                    table.ForeignKey(
                        name: "FK_effect_parameter_schema_effect_parameter_schema_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "led",
                        principalTable: "effect_parameter_schema",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_effect_parameter_schema_effect_type_effect_type_id",
                        column: x => x.effect_type_id,
                        principalSchema: "led",
                        principalTable: "effect_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_effect_parameter_schema_parameter_data_type_parameter_data_~",
                        column: x => x.parameter_data_type_id,
                        principalSchema: "led",
                        principalTable: "parameter_data_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "device",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ip_address = table.Column<IPAddress>(type: "inet", nullable: false),
                    serial_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_seen_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device", x => x.id);
                    table.ForeignKey(
                        name: "FK_device_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "led",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "tenant_user",
                schema: "led",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_user", x => new { x.tenant_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_tenant_user_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "led",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tenant_user_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "led",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "led_strip",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_id = table.Column<Guid>(type: "uuid", nullable: false),
                    led_strip_type_id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    gpio_pin = table.Column<short>(type: "smallint", nullable: false),
                    led_count = table.Column<short>(type: "smallint", nullable: false),
                    frequency = table.Column<int>(type: "integer", nullable: false),
                    dma_channel = table.Column<short>(type: "smallint", nullable: false),
                    brightness = table.Column<short>(type: "smallint", nullable: false),
                    invert = table.Column<bool>(type: "boolean", nullable: false),
                    voltage = table.Column<short>(type: "smallint", nullable: false),
                    max_current_ma = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_led_strip", x => x.id);
                    table.ForeignKey(
                        name: "FK_led_strip_device_device_id",
                        column: x => x.device_id,
                        principalSchema: "led",
                        principalTable: "device",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_led_strip_led_strip_type_led_strip_type_id",
                        column: x => x.led_strip_type_id,
                        principalSchema: "led",
                        principalTable: "led_strip_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_led_strip_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "led",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "active_state",
                schema: "led",
                columns: table => new
                {
                    led_strip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_id = table.Column<Guid>(type: "uuid", nullable: false),
                    scene_id = table.Column<Guid>(type: "uuid", nullable: false),
                    source = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    activated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active_state", x => x.led_strip_id);
                    table.ForeignKey(
                        name: "FK_active_state_device_device_id",
                        column: x => x.device_id,
                        principalSchema: "led",
                        principalTable: "device",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_active_state_led_strip_led_strip_id",
                        column: x => x.led_strip_id,
                        principalSchema: "led",
                        principalTable: "led_strip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_active_state_scene_scene_id",
                        column: x => x.scene_id,
                        principalSchema: "led",
                        principalTable: "scene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_active_state_tenant_tenant_id",
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
                    parameter_json = table.Column<string>(type: "jsonb", maxLength: 1280, nullable: true),
                    parameter_json_schema_version = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "led_state",
                schema: "led",
                columns: table => new
                {
                    effect_id = table.Column<Guid>(type: "uuid", nullable: false),
                    led_index = table.Column<short>(type: "smallint", nullable: false),
                    red = table.Column<short>(type: "smallint", nullable: false),
                    green = table.Column<short>(type: "smallint", nullable: false),
                    blue = table.Column<short>(type: "smallint", nullable: false),
                    white = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_led_state", x => new { x.effect_id, x.led_index });
                    table.ForeignKey(
                        name: "FK_led_state_effect_effect_id",
                        column: x => x.effect_id,
                        principalSchema: "led",
                        principalTable: "effect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "led",
                table: "led_strip_type",
                columns: new[] { "id", "name" },
                values: new object[] { (short)1, "SK6812 RGBW" });

            migrationBuilder.InsertData(
                schema: "led",
                table: "parameter_data_type",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { (short)1, "Property represents a true or false value", "True/ False" },
                    { (short)2, "Property represents a whole number. Ex., 1, 2, 3, ...", "Whole Number" },
                    { (short)3, "Property represents a whole or floating point number. Ex., 1, 2, 2.5, 3.8, ...", "Rational Number" },
                    { (short)4, "Property represents a collection of alpha-numeric characters. Ex., \"abc\", \"123abc\", ...", "Collection" },
                    { (short)5, "Property represents the parent to one or more child properties", "Complex" }
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
                name: "IX_active_state_device_id",
                schema: "led",
                table: "active_state",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "IX_active_state_scene_id",
                schema: "led",
                table: "active_state",
                column: "scene_id");

            migrationBuilder.CreateIndex(
                name: "IX_active_state_tenant_id",
                schema: "led",
                table: "active_state",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_device_serial_number",
                schema: "led",
                table: "device",
                column: "serial_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_tenant_id",
                schema: "led",
                table: "device",
                column: "tenant_id");

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
                name: "IX_effect_parameter_schema_effect_type_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "effect_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_effect_type_id_parent_id_key",
                schema: "led",
                table: "effect_parameter_schema",
                columns: new[] { "effect_type_id", "parent_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_parameter_data_type_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "parameter_data_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_parent_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_effect_type_name",
                schema: "led",
                table: "effect_type",
                column: "name",
                filter: "tenant_id IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_effect_type_tenant_id_name",
                schema: "led",
                table: "effect_type",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_device_id",
                schema: "led",
                table: "led_strip",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_device_id_dma_channel",
                schema: "led",
                table: "led_strip",
                columns: new[] { "device_id", "dma_channel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_device_id_gpio_pin",
                schema: "led",
                table: "led_strip",
                columns: new[] { "device_id", "gpio_pin" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_led_strip_type_id",
                schema: "led",
                table: "led_strip",
                column: "led_strip_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_tenant_id",
                schema: "led",
                table: "led_strip",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_tenant_id_name",
                schema: "led",
                table: "led_strip",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_scene_tenant_id",
                schema: "led",
                table: "scene",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_scene_tenant_id_name",
                schema: "led",
                table: "scene",
                columns: new[] { "tenant_id", "name" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_tenant_user_tenant_id",
                schema: "led",
                table: "tenant_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_user_user_id",
                schema: "led",
                table: "tenant_user",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "active_state",
                schema: "led");

            migrationBuilder.DropTable(
                name: "effect_parameter_schema",
                schema: "led");

            migrationBuilder.DropTable(
                name: "led_state",
                schema: "led");

            migrationBuilder.DropTable(
                name: "schedule",
                schema: "led");

            migrationBuilder.DropTable(
                name: "tenant_user",
                schema: "led");

            migrationBuilder.DropTable(
                name: "parameter_data_type",
                schema: "led");

            migrationBuilder.DropTable(
                name: "effect",
                schema: "led");

            migrationBuilder.DropTable(
                name: "schedule_type",
                schema: "led");

            migrationBuilder.DropTable(
                name: "users",
                schema: "led");

            migrationBuilder.DropTable(
                name: "effect_type",
                schema: "led");

            migrationBuilder.DropTable(
                name: "led_strip",
                schema: "led");

            migrationBuilder.DropTable(
                name: "scene",
                schema: "led");

            migrationBuilder.DropTable(
                name: "device",
                schema: "led");

            migrationBuilder.DropTable(
                name: "led_strip_type",
                schema: "led");

            migrationBuilder.DropTable(
                name: "tenant",
                schema: "led");
        }
    }
}
