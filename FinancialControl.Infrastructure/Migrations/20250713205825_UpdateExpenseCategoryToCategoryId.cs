﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseCategoryToCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Expenses");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Expenses",
                type: "uuid",
                maxLength: 100,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Expenses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
