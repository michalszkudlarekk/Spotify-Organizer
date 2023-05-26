namespace SpotifyOrganizer.Models;
/// <summary>
/// Class <c>Album</c> is a model for albums saved in the database.
/// The class defines properties for album details such as Id and AlbumName.
/// The properties are decorated with the Required attribute to ensure that they are not null or empty.
/// The class also includes an AlbumSongs property, which represent the songs in the album.
/// The class includes an UserAlbums property, which is currently not used.
/// </summary>
public sealed class Album
{
    public int Id { get; set; }
    public string AlbumName { get; set; } = null!;
    public ICollection<AlbumSong> AlbumSongs { get; set; } = null!;
    public ICollection<UserAlbum> UserAlbums { get; set; } = null!;
}