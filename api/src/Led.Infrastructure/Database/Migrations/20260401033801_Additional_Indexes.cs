using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Additional_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_effect_parameter_schema_id_effect_type_id",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "led",
                table: "effect_type",
                newName: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_scene_tenant_id_name",
                schema: "led",
                table: "scene",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_effect_type_tenant_id_name",
                schema: "led",
                table: "effect_type",
                columns: new[] { "tenant_id", "name" },
                unique: true,
                filter: "WHERE tenant_id IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_effect_type_id_key",
                schema: "led",
                table: "effect_parameter_schema",
                columns: new[] { "effect_type_id", "key" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_scene_tenant_id_name",
                schema: "led",
                table: "scene");

            migrationBuilder.DropIndex(
                name: "IX_effect_type_tenant_id_name",
                schema: "led",
                table: "effect_type");

            migrationBuilder.DropIndex(
                name: "IX_effect_parameter_schema_effect_type_id_key",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "led",
                table: "effect_type",
                newName: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_id_effect_type_id",
                schema: "led",
                table: "effect_parameter_schema",
                columns: new[] { "id", "effect_type_id" },
                unique: true);
        }
    }
}
