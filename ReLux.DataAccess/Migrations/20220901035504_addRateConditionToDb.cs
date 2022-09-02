using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReLux.DataAccess.Migrations
{
    public partial class addRateConditionToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RateCondition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RateCondition");
        }
    }
}
