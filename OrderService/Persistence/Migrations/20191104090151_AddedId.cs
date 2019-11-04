using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedProduct",
                table: "OrderedProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderedProduct",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedProduct",
                table: "OrderedProduct",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedProduct",
                table: "OrderedProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderedProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedProduct",
                table: "OrderedProduct",
                column: "ProductId");
        }
    }
}
