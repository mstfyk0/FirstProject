using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable


namespace Infrastructure.Data.Migrations
{
    public partial class CreateUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.CreateTable(
                    name: "Users",
                    columns: table => new
                    {
                        userName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                        password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                        emailAddress = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                        role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                        created = table.Column<DateTime>(type: "datetime2", nullable: false),
                        createdBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        lastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                        lastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Users", x => x.userName);
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
                    name: "Users"
                    );
            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }
    }
}
