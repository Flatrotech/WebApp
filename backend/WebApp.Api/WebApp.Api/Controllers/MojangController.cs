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
    public class MojangController : ControllerBase
    {
        IMojangService _mojangService;

        public MojangController(IMojangService mojangService)
        {
            _mojangService = mojangService;
        }

        [HttpGet("playerProfile/{username}")]
        [AllowAnonymous]
        public async Task<ActionResult<PlayerProfile>> GetPlayer(string username)
        {
            try
            {
                var player = await _mojangService.GetPlayer(username).ConfigureAwait(false);
                return Ok(player);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
