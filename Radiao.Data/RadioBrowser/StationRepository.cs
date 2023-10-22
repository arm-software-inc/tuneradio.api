using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using System.Net.Http.Json;
using System.Text;

namespace Radiao.Data.RadioBrowser
{
    public class StationRepository : IStationRepository
    {
        private readonly ILogger<StationRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public StationRepository(
            ILogger<StationRepository> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var url = _configuration.GetSection("RadioBrowser")["Url"];

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url!);            
        }

        public async Task<Station?> Get(string id)
        {
            var response = await _httpClient.GetAsync($"/json/stations/byuuid?uuids={id}", new CancellationToken());

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError($"Ocorreu um erro ao buscar a estação! {response?.StatusCode} - {response?.ReasonPhrase}");
                return null;
            }

            var content = await response.Content.ReadFromJsonAsync<List<Station>>();

            if (content == null || content.Count == 0)
            {
                return null;
            }

            return content.FirstOrDefault();
        }

        public async Task<List<Station>> GetAll()
        {
            var response = await _httpClient.GetAsync("/json/stations?limit=10&country=BR", new CancellationToken());

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError($"Ocorreu um erro ao buscar as estações! {response?.StatusCode} - {response?.ReasonPhrase}");
                return new();
            }

            var content = await response.Content.ReadFromJsonAsync<List<Station>>();

            return content ?? new();
        }

        public async Task<List<Station>> GetByCategory(string categories)
        {
            var response = await SearchStations(new SearchParams
            {
                TagList = categories
            });

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError($"Ocorreu um erro ao buscar as estações! {response?.StatusCode} - {response?.ReasonPhrase}");
                return new();
            }

            var content = await response.Content.ReadFromJsonAsync<List<Station>>();

            return content ?? new();
        }

        public async Task<List<Station>> GetPopular()
        {
            var response = await SearchStations(new SearchParams
            {
                CountryCode = SearchCountryCode.BR,
                Order = SearchOrder.Votes
            });

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError($"Ocorreu um erro ao buscar as estações! {response?.StatusCode} - {response?.ReasonPhrase}");
                return new();
            }

            var content = await response.Content.ReadFromJsonAsync<List<Station>>();

            return content ?? new();
        }

        public async Task<List<Station>> GetTrending()
        {
            var response = await SearchStations(new SearchParams
            {
                CountryCode = SearchCountryCode.BR,
                Order = SearchOrder.Clickcount
            });

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError($"Ocorreu um erro ao buscar as estações! {response?.StatusCode} - {response?.ReasonPhrase}");
                return new();
            }

            var content = await response.Content.ReadFromJsonAsync<List<Station>>();

            return content ?? new();
        }

        private async Task<HttpResponseMessage> SearchStations(SearchParams param)
        {
            var query = new StringBuilder();
            query.Append($"limit={param.Limit}");
            query.Append($"&countrycode={param.CountryCode}");
            query.Append($"&order={param.Order}");
            query.Append($"&reverse={param.Reverse}");
            query.Append($"&hidebroken={param.HideBroken}");

            if (param.TagList.Length > 0)
            {
                query.Append($"&tagList={param.TagList}");
            }

            return await _httpClient.GetAsync($"/json/stations/search?{query.ToString()}", new CancellationToken());
        }
    }
}
