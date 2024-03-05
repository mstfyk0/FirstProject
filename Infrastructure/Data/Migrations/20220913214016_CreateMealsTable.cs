using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Migrations
{
    public partial class CreateMealsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.CreateTable(
                    name: "Meals",
                    columns: table => new
                    {
                        id = table.Column<Guid>(type: "uniquedentifier" ,nullable: false),
                        name = table.Column<string>(type: "nvarchar(100)", maxLength:100 , nullable: false),
                        wieghtInGrams= table.Column<int>(type: "int", nullable: false),
                        calories= table.Column<decimal>(type: "decimal(18,2)", nullable:false),
                        protein= table.Column<decimal>(type: "decimal(18,2)", nullable:false),
                        carbonhydrates = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        ingredients = table.Column<string>(type: "nvarchar(1000)", maxLength:1000, nullable: false),
                        recipe = table.Column<string>(type: "nvarchar(1000)", maxLength:1000, nullable: false),
                        created = table.Column<DateTime>(type: "datetime2", nullable: false),
                        createdBy = table.Column<string>(type: "nvarchar(max)",  nullable: false),
                        lastModified = table.Column<DateTime>(type: "datetime2",  nullable: false),
                        lastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Meals", x => x.id);
                    }
                    );
            }
            catch (Exception)
            {

               
            }
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            try
            {
               migrationBuilder.DropTable(
               name: "Meals"
               );
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
           

        }
    }
}
