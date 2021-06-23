using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Api.Contracts
{
    public interface ISpotifyService
    {
        Task<IEnumerable<SimplePlaylist>> GetPlaylistsAsync();
    }
}
