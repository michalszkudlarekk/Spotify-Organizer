using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class InitofDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumName",
                table: "AlbumsSong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SongName",
                table: "AlbumsSong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumName",
                table: "AlbumsSong");

            migrationBuilder.DropColumn(
                name: "SongName",
                table: "AlbumsSong");
        }
    }
}
