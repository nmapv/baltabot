using BaltaBot.Domain.Entities;
using BaltaBot.Domain.ExternalServices;
using Newtonsoft.Json;

namespace BaltaBot.Domain.Infra.ExternalServices
{
    public class PremiumService : IPremiumService
    {
        private readonly HttpClient _httpClient;

        public PremiumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Premium?> Get(Guid id, Person person)
        {
            var body = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/nmapv/doc/main/baltabot");
            var response = JsonConvert.DeserializeAnonymousType(body, new { id = string.Empty, startedAt = DateTime.Now, closedAt = DateTime.Now });

            if (response == null)
            {
                return null;
            }

            var premium = new Premium(response.id, response.startedAt, response.closedAt, person);

            if (premium.Expired())
                return null;

            return premium;
        }
    }
}
