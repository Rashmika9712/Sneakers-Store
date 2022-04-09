using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakersStore.DataAccess.Migrations
{
    public partial class addShoeItemToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ShoeTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoeItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeItem_ShoeType_ShoeTypeId",
                        column: x => x.ShoeTypeId,
                        principalTable: "ShoeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeItem_CategoryId",
                table: "ShoeItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeItem_ShoeTypeId",
                table: "ShoeItem",
                column: "ShoeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeItem");
        }
    }
}
