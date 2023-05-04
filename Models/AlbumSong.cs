namespace SpotifyOrganizer.Models;

public sealed class AlbumSong
{
    public int Id { get; set; }
    public int SongId { get; set; }
    public int AlbumId { get; set; }
    public Song Song { get; set; } = null!;
    public Album Album { get; set; } = null!;
}