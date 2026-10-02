using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PizzApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialtyPizzaIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialtyPizzaIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpecialtyPizzaId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtyPizzaIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialtyPizzaIngredients_SpecialtyPizzas_SpecialtyPizzaId",
                        column: x => x.SpecialtyPizzaId,
                        principalTable: "SpecialtyPizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtyPizzaIngredients_SpecialtyPizzaId_SortOrder",
                table: "SpecialtyPizzaIngredients",
                columns: new[] { "SpecialtyPizzaId", "SortOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialtyPizzaIngredients");
        }
    }
}
