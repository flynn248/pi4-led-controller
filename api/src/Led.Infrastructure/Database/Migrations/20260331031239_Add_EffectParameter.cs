using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_EffectParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "effect_parameter_schema",
                schema: "led",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    effect_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    parameter_data_type_id = table.Column<short>(type: "smallint", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    min_value = table.Column<double>(type: "double precision", nullable: true),
                    max_value = table.Column<double>(type: "double precision", nullable: true),
                    allowed_values = table.Column<string>(type: "character varying(1280)", maxLength: 1280, nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effect_parameter_schema", x => x.id);
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

            migrationBuilder.InsertData(
                schema: "led",
                table: "parameter_data_type",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { (short)1, "Property represents a true or false value", "True/ False" },
                    { (short)2, "Property represents a whole number. Ex., 1, 2, 3, ...", "Whole Number" },
                    { (short)3, "Property represents a whole or floating point number. Ex., 1, 2, 2.5, 3.8, ...", "Rational Number" },
                    { (short)4, "Property represents a collection of alpha-numeric characters. Ex., \"abc\", \"123abc\", ...", "Collection" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_effect_type_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "effect_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_id_effect_type_id",
                schema: "led",
                table: "effect_parameter_schema",
                columns: new[] { "id", "effect_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_effect_parameter_schema_parameter_data_type_id",
                schema: "led",
                table: "effect_parameter_schema",
                column: "parameter_data_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "effect_parameter_schema",
                schema: "led");

            migrationBuilder.DropTable(
                name: "parameter_data_type",
                schema: "led");
        }
    }
}
