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
            var response = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/nmapv/doc/main/baltabot");
            var premium = JsonConvert.DeserializeObject<Premium>(response);

            if (premium == null )
            {
                return null;
            }

            premium.SetPerson(person);
            return premium;
        }
    }
}
