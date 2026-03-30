using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_LedState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "led_state",
                schema: "led");
        }
    }
}
