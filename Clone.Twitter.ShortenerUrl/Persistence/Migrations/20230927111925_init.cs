using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clone.Twitter.ShortenerUrl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(2039)", maxLength: 2039, nullable: false),
                    ExpirationOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ShortenCode",
                table: "Tags",
                column: "ShortenCode",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
