using Microsoft.AspNetCore.Mvc;
using SC_statistic.Services.Interfaces;
using SC_statistic.Services.Services;
using System.Net;

namespace SC_statistic.Controllers
{
    public class PlayerHistoryController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerHistoryController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("PlayerHistory")]
        public IActionResult PlayerHistory() => View();


        [HttpGet]
        public async Task<JsonResult> GetPlayerHistory([FromQuery] string nickname)
        {
            var response = await _playerService.GetPlayerHistory(nickname);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}