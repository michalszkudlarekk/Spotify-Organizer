using SpotifyAPI.Web;


namespace SpotifyOrganizer.Services;

/// <summary>
/// The <c>SpotifyApiService</c> class contains a constructor that validates credentials and creates a SpotifyClient object.
/// <c>SearchTrack</c> is a method that takes a string as input and returns a FullTrack object.
/// It creates a SearchRequest object using the input and sets the limit to 1, which means that the method will return the first track that matches the search query.
/// </summary>

public class SpotifyApiService
{
    private readonly SpotifyClient _spotify;

    public SpotifyApiService()
    {
        const string clientId = "d4408e36976a43c0ba3897a58abe5ca9";
        const string clientSecret = "be6e1b301a244883b442d5efbc67557c";
        var config = SpotifyClientConfig
            .CreateDefault()
            .WithAuthenticator(new ClientCredentialsAuthenticator(clientId, clientSecret));

        _spotify = new SpotifyClient(config);
    }
    
    public async Task<FullTrack?> SearchTrack(string songName)
    {
        var request = new SearchRequest(SearchRequest.Types.All, songName);
        request.Limit = 1;
        var searchResponse = await _spotify.Search.Item(request);
        return searchResponse.Tracks.Items?.Count == 0 ? null : searchResponse.Tracks.Items?[0];
    }


}