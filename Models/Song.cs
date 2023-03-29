namespace SpotifyOrganizer.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string SpotifyId { get; set; } = null!;
        public string SongName { get; set; } = null!;
        public string Artist { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }
        public DateTime AddDate { get; set; }

        public virtual ICollection<AlbumSong> AlbumSongs { get; set; } = null!;
        public virtual ICollection<SongGenre> SongGenres { get; set; } = null!;

    }
}
