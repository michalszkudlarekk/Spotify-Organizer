namespace SpotifyOrganizer.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string AlbumName { get; set; } = null!;

        public virtual ICollection<UserAlbum> UserAlbums { get; set; } = null!;
        public virtual ICollection<AlbumSong> AlbumSongs { get; set; } = null!;
    }
}