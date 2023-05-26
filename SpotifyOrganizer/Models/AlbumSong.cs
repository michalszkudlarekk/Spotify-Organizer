namespace SpotifyOrganizer.Models;
/// <summary>
/// The <c>AlbumSong</c> class is a model that represents the many-to-many relationship
/// between the <c>Album</c> and <c>Song</c> models in the database.
/// This class includes a foreign key reference to the <c>SongId</c> and <c>AlbumId</c> properties,
/// which allows us to retrieve information about which song belongs to which album.
/// The properties are decorated with the Required attribute to ensure that they are not null or empty.
/// The class also includes a <c>Song</c> property of type <c>Song</c>
/// and a <c>Album</c> property of type <c>Album</c>, that represent the song and album entities.
/// </summary>
public sealed class AlbumSong
{
    public int Id { get; set; }
    public int SongId { get; set; }
    public int AlbumId { get; set; }
    public Song Song { get; set; } = null!;
    public Album Album { get; set; } = null!;
}