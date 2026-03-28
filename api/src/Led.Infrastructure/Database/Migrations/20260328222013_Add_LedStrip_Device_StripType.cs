using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_LedStrip_Device_StripType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "device",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hostname = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
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
                name: "led_strip_type",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_led_strip_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "led_strip",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_id = table.Column<Guid>(type: "uuid", nullable: false),
                    led_strip_type_id = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_device_tenant_id",
                schema: "led",
                table: "device",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_device_id",
                schema: "led",
                table: "led_strip",
                column: "device_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "led_strip",
                schema: "led");

            migrationBuilder.DropTable(
                name: "device",
                schema: "led");

            migrationBuilder.DropTable(
                name: "led_strip_type",
                schema: "led");
        }
    }
}
