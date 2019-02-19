using Microsoft.EntityFrameworkCore.Migrations;

namespace bankAccount.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Decimal",
                table: "MyTransactions",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Decimal",
                table: "MyTransactions",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
