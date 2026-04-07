using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class EffParamSchema_AddParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_effect_parameter_schema_effect_type_id_key",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                schema: "led",
                table: "effect_parameter_schema",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "led",
                table: "parameter_data_type",
                columns: new[] { "id", "description", "name" },
                values: new object[] { (short)5, "Property represents the parent to one or more child properties", "Complex" });

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_effect_type_id_parent_id_key",
                schema: "led",
                table: "effect_parameter_schema",
                columns: new[] { "effect_type_id", "parent_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_parent_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_effect_parameter_schema_effect_parameter_schema_parent_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "parent_id",
                principalSchema: "led",
                principalTable: "effect_parameter_schema",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_effect_parameter_schema_effect_parameter_schema_parent_id",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.DropIndex(
                name: "IX_effect_parameter_schema_effect_type_id_parent_id_key",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.DropIndex(
                name: "IX_effect_parameter_schema_parent_id",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.DeleteData(
                schema: "led",
                table: "parameter_data_type",
                keyColumn: "id",
                keyValue: (short)5);

            migrationBuilder.DropColumn(
                name: "parent_id",
                schema: "led",
                table: "effect_parameter_schema");

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_effect_type_id_key",
                schema: "led",
                table: "effect_parameter_schema",
                columns: new[] { "effect_type_id", "key" },
                unique: true);
        }
    }
}
