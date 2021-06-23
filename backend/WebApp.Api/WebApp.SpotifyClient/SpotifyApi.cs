using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.SpotifyAPIClient
{
    public class SpotifyApi : ISpotifyApi
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

        public async Task<List<SimplePlaylist>> GetPlaylistsAsync()
        {
            try
            {
                var playlists = (await _client.Playlists.CurrentUsers().ConfigureAwait(false)).Items;
                var currentUsername = (await _client.UserProfile.Current()).DisplayName;

                return playlists.Where(p => p.Owner.DisplayName == currentUsername).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
