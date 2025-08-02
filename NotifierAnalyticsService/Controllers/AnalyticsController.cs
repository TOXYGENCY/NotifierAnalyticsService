using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace NotifierAnalyticsService.Controllers
{
    [Route("api/v1/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ILogger<AnalyticsController> logger;
        private readonly IDatabase redis;

        public AnalyticsController(ILogger<AnalyticsController> logger, IDatabase cache)
        {
            this.logger = logger;
            this.redis = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationStatusesStats()
        {
            try
            {
                var result = await redis.StreamRangeAsync("analytics", "-", "+", 1000, Order.Descending);

                return Ok(result);
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
