using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public class EditingForEntityProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.AlterColumn<string>(
                    name: "lastModifiedBy",
                    table: "Products",
                    type: "nvarchar(max)",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(max)"
                    );

                migrationBuilder.AlterColumn<string>(
                    name: "createdBy",
                    table: "Products",
                    type: "nvarchar(max)",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(max)"
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
                migrationBuilder.AlterColumn<string>(
                    name: "lastModifiedBy",
                    table: "Products",
                    type: "nvarchar(max)",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(max)"
                    );

                migrationBuilder.AlterColumn<string>(
                    name: "createdBy",
                    table: "Products",
                    type: "nvarchar(max)",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "nvarchar(max)"
                    );

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }
    }
}
