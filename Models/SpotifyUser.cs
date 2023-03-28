namespace SpotifyOrganizer.Models
{
    public class SpotifyUser
    {
        public int Id { get; set; }

        public string? UserName { get; set; }
        public string UserId { get; set; } = null!;

        public virtual ICollection<UserAlbum> UserAlbums { get; set; } = null!;
    }
}
