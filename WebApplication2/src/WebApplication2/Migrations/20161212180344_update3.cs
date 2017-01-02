using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heinbo.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_AspNetUsers_UserId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_UserId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Order",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderItem",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_UserId",
                table: "OrderItem",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_AspNetUsers_UserId",
                table: "OrderItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
