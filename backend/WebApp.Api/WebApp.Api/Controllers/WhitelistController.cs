using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Api.Contracts;
using WebApp.Api.Models;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhitelistController : ControllerBase
    {
        IWhitelistService _whitelistService;

        public WhitelistController(IWhitelistService whitelistService)
        {
            _whitelistService = whitelistService;
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(WhitelistModel whitelist)
        {
            try
            {
                var player = await _whitelistService.AddToWhitelist(whitelist).ConfigureAwait(false);
                return Ok(player);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
