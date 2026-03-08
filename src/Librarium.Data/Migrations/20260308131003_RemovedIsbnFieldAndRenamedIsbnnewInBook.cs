using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIsbnFieldAndRenamedIsbnnewInBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Isbn", table: "Books");
            migrationBuilder.RenameColumn(name: "IsbnNew", newName: "Isbn", table: "Books");
        }
        
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Isbn", newName: "IsbnNew", table: "Books");

            migrationBuilder.AddColumn<long>(
                name: "Isbn",
                table: "Books",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
