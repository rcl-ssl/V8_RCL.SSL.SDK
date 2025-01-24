# nullable disable

using System.Text;
using System.Text.Json;

namespace RCL.SSL.SDK
{
    internal abstract class ApiRequestBase
    {
        protected static readonly HttpClient _client;

        static ApiRequestBase()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromMinutes(10);
        }

        public void SetClientHeaders(string apiKey, string source = "")
        {
            _client.DefaultRequestHeaders.Clear();

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            if(!string.IsNullOrEmpty(source))
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("RCL-Source", source);
            }
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
            where TResult : new()
        {
            try
            {
                var response = await _client.GetAsync(uri);

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TResult obj = JsonSerializer.Deserialize<TResult>(content);
                    return obj;
                }
                else
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TResult> PostAsync<T, TResult>(string uri, T payload)
            where TResult : new()
            where T : class
        {
            try
            {
                var response = await _client.PostAsync(uri,
                     new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TResult obj = JsonSerializer.Deserialize<TResult>(content);
                    return obj;
                }
                else
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task PostAsync<T>(string uri, T payload)
        where T : class
        {
            try
            {
                var response = await _client.PostAsync(uri,
                     new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task DeleteAsync(string uri)
        {
            try
            {
                var response = await _client.DeleteAsync(uri);

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ResolveContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            else
            {
                if (content.ToLower().Contains("!doctype html"))
                {
                    return string.Empty;
                }
                else
                {
                    return content;
                }
            }
        }

    }
}