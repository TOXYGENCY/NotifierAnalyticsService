using Microsoft.AspNetCore.Mvc;
using NotifierAnalyticsService.Interfaces;

namespace NotifierAnalyticsService.Controllers
{
    [Route("api/v1/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ILogger<AnalyticsController> logger;
        private readonly INotificationStatusesCache cache;

        public AnalyticsController(ILogger<AnalyticsController> logger, INotificationStatusesCache cache)
        {
            this.logger = logger;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestStatuses(uint entriesAmount)
        {
            try
            {
                return Ok(await cache.GetLatestStatusesAsync(entriesAmount));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred while retrieving notification statistics.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Возникла непредвиденная ошибка при получении статистики уведомлений. Обратитесь к администратору или попробуйте позже.");
            }
        }
    }
}
