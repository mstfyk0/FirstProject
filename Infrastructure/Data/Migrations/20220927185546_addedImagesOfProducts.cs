using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Migrations
{
    public partial class addedImagesOfProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.AddColumn<string>(
                    name: "productPhotoPath",
                    table: "Products",
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
                    name: "productPhotoPath",
                    table: "Products"
                    );
            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }
    }
}
