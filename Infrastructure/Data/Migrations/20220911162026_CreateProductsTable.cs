
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class CreateProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.CreateTable(
                    name: "Products",
                    columns: table => new
                    {
                        id = table.Column<Guid>(type: "uniqueidentifer", nullable: false),
                        name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                        weightGrams = table.Column<int>(type: "int", nullable: false),
                        calories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        carbonhydrates = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        ingredients = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                        description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),


                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Products", x => x.id);

                    });
            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.DropTable(
                name: "Products"
                );
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
