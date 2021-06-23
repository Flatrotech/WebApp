using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Contracts;
using WebApp.Core.Models;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyController : ControllerBase
    {
        ISpotifyService _spotifyService;

        //private readonly ILogger _logger;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
            //_logger = logger;
        }

        [HttpGet("playlist")]
        [AllowAnonymous]
        public async Task<ActionResult<SimplePlaylist>> GetPlaylists()
        {
            try
            {
                var playlists = await _spotifyService.GetPlaylistsAsync().ConfigureAwait(false);
                return Ok(playlists);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: { e.Message }");
                return StatusCode(500, e.Message);
            }
        }
    }
}
