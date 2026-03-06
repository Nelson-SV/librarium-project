using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusFieldInLoanAsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);
            
            migrationBuilder.Sql("UPDATE Loans SET Status = 'Returned' WHERE ReturnDate IS NOT NULL");
            migrationBuilder.Sql("UPDATE Loans SET Status = 'Active' WHERE ReturnDate IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Loans");
        }
    }
}
