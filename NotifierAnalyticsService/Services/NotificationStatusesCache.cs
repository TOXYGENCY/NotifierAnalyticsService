using NotifierAnalyticsService.Interfaces;
using NotifierAnalyticsService.Models;
using StackExchange.Redis;

namespace NotifierAnalyticsService.Services
{
    public class NotificationStatusesCache : INotificationStatusesCache
    {
        private readonly IDatabase redis;

        public NotificationStatusesCache(IDatabase redis)
        {
            this.redis = redis;
        }

        private IEnumerable<NotificationStatusEntry> ExtractEntries(StreamEntry[] streamEntries)
        {
            return streamEntries.Select(e =>
                new NotificationStatusEntry(e.Id, (string)e["notificationId"], (string)e["statusId"]));
        }

        public async Task<IEnumerable<NotificationStatusEntry>> GetLatestStatusesAsync(uint entriesAmount)
        {
            entriesAmount = entriesAmount <= 0 ? 100 : entriesAmount;
            var raw = await redis.StreamRangeAsync("analytics", "-", "+", (int)entriesAmount, Order.Descending);
            return ExtractEntries(raw);
        }
    }
}
