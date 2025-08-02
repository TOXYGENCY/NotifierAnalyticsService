using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NotifierAnalyticsService.Controllers
{
    [Route("api/analytics/v1")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ILogger<AnalyticsController> logger;

        public AnalyticsController(ILogger<AnalyticsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationStatusesStats()
        {
            try
            {
                throw new NotImplementedException();

                return Ok();
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
