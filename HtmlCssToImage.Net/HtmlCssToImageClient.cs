using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlCssToImage.Net
{
    public class HtmlCssToImageClient : IDisposable
    {
        private const string ApiEndpoint = "https://hcti.io/v1";
        private readonly HttpClient _client;

        public HtmlCssToImageClient(
            HtmlCssToImageCredentials credentials,
            HttpClient client = null)
        {
            _client = client;
            _client ??= new HttpClient();
            
            string encodedCredentials = Convert
                .ToBase64String(
                    Encoding.ASCII.GetBytes($"{credentials.UserId}:{credentials.ApiKey}"));
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
        }

        public async Task<string> CreateImageAsync(
            CreateImageParameters parameters,
            CancellationToken cancellationToken = default)
        {
            parameters.Validate();

            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync(
                $"{ApiEndpoint}/image",
                parameters,
                cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content
                    .ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken);

                throw new HtmlCssToImageException(errorResponse);
            }

            var response = await httpResponse.Content
                .ReadFromJsonAsync<CreateImageResponse>(cancellationToken: cancellationToken);

            return response.Url;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}