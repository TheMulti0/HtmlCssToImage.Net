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
            CreateImageRequest request,
            CancellationToken cancellationToken = default)
        {
            request.Validate();

            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync(
                $"{ApiEndpoint}/image",
                request,
                cancellationToken);

            await httpResponse.EnsureSuccessAsync(cancellationToken);

            return await httpResponse.Content
                .ReadFromJsonAsync<CreateImageResponse>(cancellationToken: cancellationToken);
        }

        public async Task<Stream> GetImageAsync(
            GetImageRequest request,
            CancellationToken cancellationToken = default)
        {
            request.Validate();

            string query = GetQueryString(request);

            string extension = request.Format.ToString().ToLower();

            var httpResponse = await _client.GetAsync(
                $"{ApiEndpoint}/image/{request.Id}.{extension}?{query}",
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

        private static string GetQueryString(GetImageRequest request)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            
            if (request.Width != null)
            {
                query.Add("width", request.Width.ToString());
            }
            if (request.Height != null)
            {
                query.Add("height", request.Height.ToString());
            }
            
            return query.ToString();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}