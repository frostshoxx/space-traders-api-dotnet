using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceTradersApiDotNet.Data
{
    public class SpaceTradersApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseUrl;

        public SpaceTradersApiService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _baseUrl = config.GetValue<string>("SpaceTradersApi:BaseUrl");
        }

        public async Task<ServerStatus> GetServerStatus()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseUrl}/game/status"));

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ServerStatus>(responseStream);

                return result;
            }

            return new ServerStatus { Status = $"Unable to connect to {_baseUrl}" };
        }

        public async Task<ServerStatus> GenerateAccessToken(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_baseUrl}/{username}/status"));

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ServerStatus>(responseStream);

                return result;
            }

            return new ServerStatus { Status = $"Unable to connect to {_baseUrl}" };
        }
    }
}
