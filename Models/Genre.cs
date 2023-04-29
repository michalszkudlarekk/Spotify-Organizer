namespace SpotifyOrganizer.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<SongGenre> SongGenres { get; set; } = null!;
    }
}