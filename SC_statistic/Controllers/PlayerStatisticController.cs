using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.ViewModels.PlayerStatistic;
using SC_statistic.Services.Interfaces;
using System.Security.Claims;

namespace SC_statistic.Controllers
{
    [Authorize]
    public class PlayerStatisticController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerStatisticController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("PlayerStatistic")]
        public async Task<IActionResult> PlayerStatistic()
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _playerService.GetTrackedPlayers(userId);
            return View(response.Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetTrackedPlayer([FromQuery] string nickname)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _playerService.GetTrackedPlayerStatistic(userId, nickname);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPost]
        public async Task<JsonResult> GetSession([FromBody] SessionViewModel sessionViewModel)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _playerService.GetSession(userId, sessionViewModel);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPost]
        public async Task<JsonResult> AddCheckpoint([FromBody] CheckpointViewModel checkpointViewModel)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _playerService.AddCheckpoint(userId, checkpointViewModel);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }

    }
}
