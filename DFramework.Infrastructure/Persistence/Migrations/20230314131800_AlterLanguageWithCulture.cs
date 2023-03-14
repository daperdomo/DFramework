using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFramework.Infrastructure.Persitence.Migrations
{
    public partial class AlterLanguageWithCulture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Map",
                table: "Languages",
                newName: "Culture");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Languages",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "Culture",
                table: "Languages",
                newName: "Map");
        }
    }
}
