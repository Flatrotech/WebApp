using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Contracts;
using WebApp.Core.Models;

namespace WebApp.Api.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly ISpotifyClient _client;
        private readonly IConfiguration _config;

        public SpotifyService(IConfiguration config)
        {
            _config = config;

            _client = GetClient().Result;
        }

        private async Task<ISpotifyClient> GetClient()
        {
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest(_config["CLIENT_ID"], _config["CLIENT_SECRET"]);
            var response = await new OAuthClient(config).RequestToken(request);

            return new SpotifyClient(config.WithToken(response.AccessToken));
        }

        public async Task<IEnumerable<SimplePlaylist>> GetPlaylistsAsync()
        {
            try
            {
                var playlists = (await GetClient().Result.Playlists.CurrentUsers().ConfigureAwait(false)).Items;
                var currentUsername = (await GetClient().Result.UserProfile.Current()).DisplayName;

                return playlists.Where(p => p.Owner.DisplayName == currentUsername).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
