using Microsoft.AspNetCore.Mvc;
using SC_statistic.Services.Interfaces;
using SC_statistic.Services.Services;

namespace SC_statistic.Controllers
{
    public class CorporationsController : Controller
    {
        private readonly ICorporationService _corporationService;

        public CorporationsController(ICorporationService corporationService)
        {
            _corporationService = corporationService;
        }

        [HttpGet]
        [Route("Corporations")]
        public IActionResult Corporations() => View();

        [HttpGet]
        public async Task<JsonResult> GetCorporationInfo([FromQuery] string? name, string? tag)
        {
            var response = await _corporationService.GetCorporationInfo(name, tag);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}