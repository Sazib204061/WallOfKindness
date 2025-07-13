using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MomotarJhuri.Infractructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureGiftGiftDetailandImageTableandseedsomeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    GiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftDetail_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gifts",
                columns: new[] { "Id", "Location", "Title" },
                values: new object[,]
                {
                    { 1, "Dhaka, Mirpur-10", "Birthday Gift Package" },
                    { 2, "Dhaka, Mirpur-12", "Anniversary Special" }
                });

            migrationBuilder.InsertData(
                table: "GiftDetail",
                columns: new[] { "Id", "Description", "GiftId", "Status" },
                values: new object[,]
                {
                    { 1, "Description of gift-1", 1, 0 },
                    { 2, "Description of gift-2", 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "GiftId", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 1, "images/gifts/birthday-package.jpg" },
                    { 2, 1, "images/gifts/birthday-package-alt.jpg" },
                    { 3, 2, "images/gifts/anniversary-special.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiftDetail_GiftId",
                table: "GiftDetail",
                column: "GiftId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_GiftId",
                table: "Images",
                column: "GiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftDetail");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Gifts");
        }
    }
}
