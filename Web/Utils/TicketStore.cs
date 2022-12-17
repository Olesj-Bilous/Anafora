using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AnaforaWeb.Utils
{
    public class TicketStore : ITicketStore
    {
        public TicketStore(IDistributedCache cache)
        {
            _cache = cache;
        }

        private readonly IDistributedCache _cache;
        private const string prefix = "SessionTicket-";

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            string key = prefix + Guid.NewGuid().ToString();
            await RenewAsync(key, ticket);
            return key;
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(40));
            if (ticket.Properties.ExpiresUtc.HasValue) options.SetAbsoluteExpiration(ticket.Properties.ExpiresUtc.Value);
            await _cache.SetAsync(key, TicketSerializer.Default.Serialize(ticket), options);
        }

        public async Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            var bytes = await _cache.GetAsync(key);
            var ticket = bytes == null ? null : TicketSerializer.Default.Deserialize(bytes);
            return ticket;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
