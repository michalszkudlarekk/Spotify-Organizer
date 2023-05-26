namespace SpotifyOrganizer.Models
{
    /// <summary>
    /// Class <c>Song</c> is a model for songs saved in the database.
    /// The class defines properties for Song details such as Id, SpotifyId, SongName, Artist and ReleaseDate.
    /// The properties are decorated with the Required attribute to ensure that they are not null or empty.
    /// SpotifyId is used to communicate with Spotify API.
    /// SongName, Artist and ReleaseDate are taken from Spotify API.
    /// The class also includes an Album property of type AlbumSong, which represent the songs in the album.
    /// The class includes a Song property of type SongGenres, which is currently not used.
    /// </summary>    
    
    public sealed class Song
    {
        public int Id { get; set; }

        public string SpotifyId { get; set; } = null!;
        public string SongName { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        
        
        public ICollection<AlbumSong> AlbumSongs { get; set; } = null!;
        public ICollection<SongGenre> SongGenres { get; set; } = null!;
    }
}