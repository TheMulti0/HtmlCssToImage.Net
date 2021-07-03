using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<CreateImageResponse> CreateImageAsync(
            CreateImageParameters parameters,
            CancellationToken cancellationToken = default)
        {
            parameters.Validate();

            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync(
                $"{ApiEndpoint}/image",
                parameters,
                cancellationToken);

            await httpResponse.ThrowIfFailedAsync(cancellationToken);

            var response = await httpResponse.Content
                .ReadFromJsonAsync<CreateImageResponse>(cancellationToken: cancellationToken);

            return response;
        }

        public async Task<Stream> GetImageAsync(
            GetImageParameters parameters,
            CancellationToken cancellationToken = default)
        {
            parameters.Validate();

            string query = GetQueryString(parameters);

            string extension = parameters.Format.ToString().ToLower();

            return await _client.GetStreamAsync(
                $"{ApiEndpoint}/image/{parameters.Id}.{extension}?{query}",
                cancellationToken);
        }

        private static string GetQueryString(GetImageParameters parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            
            if (parameters.Width != null)
            {
                query.Add("width", parameters.Width.ToString());
            }
            if (parameters.Height != null)
            {
                query.Add("height", parameters.Height.ToString());
            }
            
            return query.ToString();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}