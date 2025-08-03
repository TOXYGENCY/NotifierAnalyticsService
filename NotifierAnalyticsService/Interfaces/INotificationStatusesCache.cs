using NotifierAnalyticsService.Models;

namespace NotifierAnalyticsService.Interfaces
{
    public interface INotificationStatusesCache
    {
        Task<IEnumerable<NotificationStatusEntry>> GetLatestStatusesAsync(uint entriesAmount);
    }
}
