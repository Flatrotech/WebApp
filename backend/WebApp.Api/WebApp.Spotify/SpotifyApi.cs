using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using System;
using System.Threading.Tasks;

namespace WebApp.Spotify
{
    public class SpotifyApi
    {
        private readonly ISpotifyClient _client;
        private readonly IConfiguration _config;

        public SpotifyApi(IConfiguration config)
        {
            _config = config;

            _client = GetClient().Result;
        }

        private async Task<ISpotifyClient> GetClient()
        {
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("CLIENT_ID", "CLIENT_SECRET");
            var response = await new OAuthClient(config).RequestToken(request);

            return new SpotifyClient(config.WithToken(response.AccessToken));
        }
    }
}
