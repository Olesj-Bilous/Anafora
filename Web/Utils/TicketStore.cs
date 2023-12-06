using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;

namespace AnaforaWeb.Utils
{
    public class TicketStore : ITicketStore
    {
        public TicketStore(IDistributedCache cache)
        {
            _cache = cache;
        }

        private const string prefix = "SessionTicket-";
        private readonly IDistributedCache _cache;
        private readonly IDataSerializer<AuthenticationTicket> _ticketSerializer = TicketSerializer.Default;

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            string key = Guid.NewGuid().ToString();
            await RenewAsync(key, ticket);
            return key;
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(40));
            if (ticket.Properties.ExpiresUtc.HasValue) options.SetAbsoluteExpiration(ticket.Properties.ExpiresUtc.Value);
            await _cache.SetAsync($"{prefix}{key}", _ticketSerializer.Serialize(ticket), options);
        }

        public async Task<AuthenticationTicket?> RetrieveAsync(string key)
        {
            var bytes = await _cache.GetAsync($"{prefix}{key}");
            return bytes == null ? null : _ticketSerializer.Deserialize(bytes);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync($"{prefix}{key}");
        }
    }
}
