namespace SpotifyOrganizer.Models
{
    public sealed class UserAlbum
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public SpotifyUser SpotifyUser { get; set; } = null!;
        public Album Album { get; set; } = null!;
    }
}