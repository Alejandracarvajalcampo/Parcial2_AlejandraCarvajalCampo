using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertDB.Migrations
{
    /// <inheritdoc />
    public partial class CreateCodeTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_Id",
                table: "Tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "Code",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Code",
                table: "Tickets",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_Code",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Tickets");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Id",
                table: "Tickets",
                column: "Id",
                unique: true);
        }
    }
}
