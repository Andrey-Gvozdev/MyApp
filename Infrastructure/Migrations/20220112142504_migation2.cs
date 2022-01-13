using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class migation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagesSnippets_Creatives_PageId",
                table: "PagesSnippets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PagesSnippets",
                table: "PagesSnippets");

            migrationBuilder.DropColumn(
                name: "SnippetId",
                table: "PagesSnippets");

            migrationBuilder.RenameTable(
                name: "PagesSnippets",
                newName: "PageSnippet");

            migrationBuilder.RenameIndex(
                name: "IX_PagesSnippets_PageId",
                table: "PageSnippet",
                newName: "IX_PageSnippet_PageId");

            migrationBuilder.AddColumn<string>(
                name: "SnippetName",
                table: "PageSnippet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageSnippet",
                table: "PageSnippet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSnippet_Creatives_PageId",
                table: "PageSnippet",
                column: "PageId",
                principalTable: "Creatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSnippet_Creatives_PageId",
                table: "PageSnippet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageSnippet",
                table: "PageSnippet");

            migrationBuilder.DropColumn(
                name: "SnippetName",
                table: "PageSnippet");

            migrationBuilder.RenameTable(
                name: "PageSnippet",
                newName: "PagesSnippets");

            migrationBuilder.RenameIndex(
                name: "IX_PageSnippet_PageId",
                table: "PagesSnippets",
                newName: "IX_PagesSnippets_PageId");

            migrationBuilder.AddColumn<int>(
                name: "SnippetId",
                table: "PagesSnippets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PagesSnippets",
                table: "PagesSnippets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PagesSnippets_Creatives_PageId",
                table: "PagesSnippets",
                column: "PageId",
                principalTable: "Creatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
