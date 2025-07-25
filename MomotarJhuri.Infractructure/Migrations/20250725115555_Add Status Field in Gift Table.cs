using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomotarJhuri.Infractructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusFieldinGiftTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Gifts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Gifts");
        }
    }
}
