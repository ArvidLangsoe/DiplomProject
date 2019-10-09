using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class BatchAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatches_Batch_BatchId",
                table: "ProductBatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Batch",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "Batch");

            migrationBuilder.RenameTable(
                name: "Batch",
                newName: "Batches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Batches",
                table: "Batches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatches_Batches_BatchId",
                table: "ProductBatches",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatches_Batches_BatchId",
                table: "ProductBatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Batches",
                table: "Batches");

            migrationBuilder.RenameTable(
                name: "Batches",
                newName: "Batch");

            migrationBuilder.AddColumn<DateTime>(
                name: "Delivered",
                table: "Batch",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Batch",
                table: "Batch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatches_Batch_BatchId",
                table: "ProductBatches",
                column: "BatchId",
                principalTable: "Batch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
