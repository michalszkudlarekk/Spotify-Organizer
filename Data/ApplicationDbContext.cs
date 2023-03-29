using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAlbum>()
            .HasOne(u => u.SpotifyUser)
            .WithMany(ua => ua.UserAlbums)
              .HasForeignKey(ui => ui.UserId);

            builder.Entity<UserAlbum>()
               .HasOne(u => u.Album)
                .WithMany(ua => ua.UserAlbums)
                 .HasForeignKey(ui => ui.AlbumId);

            builder.Entity<AlbumSong>()
                .HasOne(u => u.Album)
                .WithMany(ua => ua.AlbumSongs)
                .HasForeignKey(ui => ui.AlbumId);

            builder.Entity<AlbumSong>()
                .HasOne(u => u.Song)
                .WithMany(ua => ua.AlbumSongs)
                .HasForeignKey(ui => ui.SongId);

            builder.Entity<SongGenre>()
                .HasOne(u => u.Song)
                .WithMany(ua => ua.SongGenres)
                .HasForeignKey(ui => ui.SongId);

            builder.Entity<SongGenre>()
                .HasOne(u => u.Genre)
                .WithMany(ua => ua.SongGenres)
                .HasForeignKey(ui => ui.GenreId);

            base.OnModelCreating(builder);
        }

        public DbSet<SpotifyUser> SpotifyUsers { get; set; } = null!;

        public DbSet<Album> Albums { get; set; } = null!;

        public DbSet<Song> Songs { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;

        public DbSet<UserAlbum> UserAlbums { get; set; } = null!;

        public DbSet<AlbumSong> AlbumsSong { get; set; } = null!;

        public DbSet<SongGenre> SongGenres { get; set; } = null!;


    }


}