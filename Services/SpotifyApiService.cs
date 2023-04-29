using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotifyOrganizer.Models;


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


    public async Task<FullTrack?> SearchTrack(string songName)
    {
        var request = new SearchRequest(SearchRequest.Types.Track,songName);
        request.Limit = 1;
        var searchResponse = await _spotify.Search.Item(request);
        return searchResponse.Tracks.Items?[0];
    }
    
}