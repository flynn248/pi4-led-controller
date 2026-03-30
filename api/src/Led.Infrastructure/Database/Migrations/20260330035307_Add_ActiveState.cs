using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_ActiveState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "active_state",
                schema: "led");
        }
    }
}
