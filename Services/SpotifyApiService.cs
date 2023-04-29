using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace SpotifyOrganizer.Services;

public class SpotifyApiService
{
    private SpotifyClient _spotify;

    public SpotifyApiService()
    {
        const string clientID = "d4408e36976a43c0ba3897a58abe5ca9";
        const string clientSecret = "be6e1b301a244883b442d5efbc67557c";
        var config = SpotifyClientConfig
            .CreateDefault()
            .WithAuthenticator(new ClientCredentialsAuthenticator(clientID, clientSecret));

        _spotify = new SpotifyClient(config);
    }

    //Tasl<IActionResult> kolekcja obiektów html

    public async Task SearchTrack()
    {
        var track = await _spotify.Tracks.Get("3n3Ppam7vgaVa1iaRUc9Lp");
        Console.WriteLine("Track: {0}", track.Name);
    }

    
}