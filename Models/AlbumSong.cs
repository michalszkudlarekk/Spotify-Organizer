﻿namespace SpotifyOrganizer.Models;

public class AlbumSong
{
    public int Id { get; set; }

    public int SongId { get; set; }
    public int AlbumId { get; set; }
    public virtual Song Song { get; set; } = null!;
    public virtual Album Album { get; set; } = null!;
}