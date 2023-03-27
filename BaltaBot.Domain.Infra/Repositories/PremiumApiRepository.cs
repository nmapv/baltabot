using BaltaBot.Domain.Entities;
using BaltaBot.Domain.Repositories;
using Newtonsoft.Json;

namespace BaltaBot.Domain.Infra.Repositories
{
    public class PremiumApiRepository : IPremiumApiRepository
    {
        private readonly HttpClient _httpClient;

        public PremiumApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Premium?> Create(Guid id, Person person)
        {
            var body = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/nmapv/doc/main/baltabot");
            var response  = JsonConvert.DeserializeAnonymousType(body, new { id = string.Empty, startedAt = DateTime.Now, closedAt = DateTime.Now });

            if (response == null)
            {
                return null;
            }

            return new(response.id, response.startedAt, response.closedAt, person);
        }
    }
}
