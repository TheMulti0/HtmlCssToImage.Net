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
    public class HtmlCssToImageClient : IHtmlCssToImageClient, IDisposable
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

            await httpResponse.EnsureSuccessAsync(cancellationToken);

            return await httpResponse.Content
                .ReadFromJsonAsync<CreateImageResponse>(cancellationToken: cancellationToken);
        }

        public async Task<Stream> GetImageAsync(
            GetImageParameters parameters,
            CancellationToken cancellationToken = default)
        {
            parameters.Validate();

            string query = GetQueryString(parameters);

            string extension = parameters.Format.ToString().ToLower();

            var httpResponse = await _client.GetAsync(
                $"{ApiEndpoint}/image/{parameters.Id}.{extension}?{query}",
                cancellationToken);

            await httpResponse.EnsureSuccessAsync(cancellationToken);
            
            return await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
        }

        public async Task DeleteImageAsync(
            string id,
            CancellationToken cancellationToken = default)
        {
            HttpResponseMessage httpResponse = await _client.DeleteAsync(
                $"{ApiEndpoint}/image/{id}",
                cancellationToken);

            await httpResponse.EnsureSuccessAsync(cancellationToken);
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