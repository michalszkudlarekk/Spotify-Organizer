namespace SpotifyOrganizer.Models
{
    public sealed class Album
    {
        public int Id { get; set; }
        public string AlbumName { get; set; } = null!;
        public ICollection<UserAlbum> UserAlbums { get; set; } = null!;
        public ICollection<AlbumSong> AlbumSongs { get; set; } = null!;
    }
}