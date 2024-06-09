using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Services.Utility
{
    public class DiscordWebhookService : IDiscordWebhookService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public DiscordWebhookService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            webhookLink = _configuration["Discord:Webhook"];
        }
        private readonly string webhookLink;

        public async Task SendLogAsync(string message)
        {
            var payload = new { content = message };
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(webhookLink, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
