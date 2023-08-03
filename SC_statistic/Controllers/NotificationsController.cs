using Microsoft.AspNetCore.Mvc;
using SC_statistic.Services.Interfaces;
using SC_statistic.DataLayer.Enums;
using SC_statistic.Services.Services;
using System.Net;
using SC_statistic.DataLayer.DTO;
using SC_statistic.DataLayer.DTO.Notifications;

namespace SC_statistic.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("Notifications")]
        public async Task<IActionResult> Notifications()
        {
            var response = await _notificationService.GetNotifications();
            if (response.StatusCode == HttpStatusCode.OK) {
                return View(response.Data);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<JsonResult> GetNotificationsWithSelectedType([FromBody] LoadNotificationsDTO model)
        {
            var response = await _notificationService.GetNotificationsWithSelectedType(model);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPost]
        public async Task<JsonResult> GetMoreNotificationsWithSelectedType([FromBody] LoadNotificationsDTO model)
        {
            var response = await _notificationService.GetNotificationsWithSelectedType(model);
            return new JsonResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
