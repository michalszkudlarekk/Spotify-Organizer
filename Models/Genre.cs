namespace SpotifyOrganizer.Models
{
    public sealed class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; } = null!;

        public ICollection<SongGenre> SongGenres { get; set; } = null!;
    }
}