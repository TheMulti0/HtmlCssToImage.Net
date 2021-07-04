using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlCssToImage.Net
{
    internal static class Extensions
    {
        public static void Validate(this CreateImageParameters p)
        {
            if (string.IsNullOrWhiteSpace(p.Html) && p.Url == null)
            {
                throw new ArgumentException("Html or Url must be set");
            }
            
            if (p.DeviceScale < 1 || p.DeviceScale > 3)
            {
                throw new ArgumentException("Device scale must be between 1 and 3");
            }
        }
        
        public static void Validate(this GetImageParameters p)
        {
            if (string.IsNullOrWhiteSpace(p.Id))
            {
                throw new ArgumentException("Id must not be null");
            }
            
            if (p.Width != null && (p.Width < 1 || p.Width > 5000))
            {
                throw new ArgumentException("Width must be between 1 and 5000");
            }
            
            if (p.Height != null && (p.Height < 1 || p.Height > 5000))
            {
                throw new ArgumentException("Height must be between 1 and 5000");
            }
        }

        public static async Task ThrowIfFailedAsync(
            this HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content
                    .ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken);

                throw new HtmlCssToImageException(errorResponse);
            }
        }
    }
}