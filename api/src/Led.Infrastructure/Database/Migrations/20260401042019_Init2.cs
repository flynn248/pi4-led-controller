using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_effect_type_name",
                schema: "led",
                table: "effect_type");

            migrationBuilder.CreateIndex(
                name: "IX_effect_type_name",
                schema: "led",
                table: "effect_type",
                column: "name",
                filter: "tenant_id IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_effect_type_name",
                schema: "led",
                table: "effect_type");

            migrationBuilder.CreateIndex(
                name: "IX_effect_type_name",
                schema: "led",
                table: "effect_type",
                column: "name",
                filter: "tenant_id IS NULL;");
        }
    }
}
