using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContentInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Contents",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Contents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Genres",
                table: "Contents",
                type: "text[]",
                nullable: false,
                defaultValueSql: "ARRAY[]::text[]");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Contents",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Contents");
        }
    }
}
