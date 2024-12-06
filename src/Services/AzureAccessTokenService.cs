#nullable disable

using Microsoft.Extensions.Options;
using System.Text.Json;

namespace RCL.SSL.SDK
{
    internal class AzureAccessTokenService : IAzureAccessTokenService
    {
        private static readonly HttpClient _client;
        private readonly IOptions<MicrosoftEntraAppOptions> _options;

        static AzureAccessTokenService()
        {
            _client = new HttpClient();
        }

        public AzureAccessTokenService(IOptions<MicrosoftEntraAppOptions> options)
        {
            _options = options;
        }

        public async Task<AzureAccessToken> GetTokenAsync(string resource)
        {
            AzureAccessToken accessToken = new AzureAccessToken();

            try
            {
                _client.DefaultRequestHeaders.Clear();

                var formcontent = new List<KeyValuePair<string, string>>();
                formcontent.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                formcontent.Add(new KeyValuePair<string, string>("client_id", _options.Value.ClientId));
                formcontent.Add(new KeyValuePair<string, string>("client_secret", _options.Value.ClientSecret));
                formcontent.Add(new KeyValuePair<string, string>("resource", resource));

                var request = new HttpRequestMessage(HttpMethod.Post, $"https://login.microsoftonline.com/{_options.Value.TenantId}/oauth2/token") { Content = new FormUrlEncodedContent(formcontent) };

                var response = await _client.SendAsync(request);
                string jstr = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    accessToken = JsonSerializer.Deserialize<AzureAccessToken>(jstr);
                }
                else
                {
                    throw new Exception($"Could not obtain auth token. {jstr}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return accessToken;
        }
    }
}
