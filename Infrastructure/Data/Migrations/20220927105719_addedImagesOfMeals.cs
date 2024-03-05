using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Migrations
{
    public partial class addedImagesOfMeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.AddColumn<string>(
                    name: "mealPhotoPath",
                    table: "Meals",
                    type: "nvarchar(max)",
                    nullable: false,
                    defaultValue: ""
                    );
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
                migrationBuilder.DropColumn(
                    name: "mealPhotoPath",
                    table:"Meals"
                    );
            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }
    }

}
