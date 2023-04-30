namespace SpotifyOrganizer.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string SpotifyId { get; set; } = null!;
        public string SongName { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        
        
        public virtual ICollection<AlbumSong> AlbumSongs { get; set; } = null!;
        public virtual ICollection<SongGenre> SongGenres { get; set; } = null!;
    }
}