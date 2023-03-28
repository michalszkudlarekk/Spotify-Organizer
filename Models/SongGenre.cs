namespace SpotifyOrganizer.Models
{
    public class SongGenre
    {
        public int Id { get; set; }

        public int SongId { get; set; }
        public int GenreId { get; set; }
        public virtual Song Song { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
