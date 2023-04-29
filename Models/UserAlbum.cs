namespace SpotifyOrganizer.Models
{
    public class UserAlbum
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public virtual SpotifyUser SpotifyUser { get; set; } = null!;
        public virtual Album Album { get; set; } = null!;
    }
}