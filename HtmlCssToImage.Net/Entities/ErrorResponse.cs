using System.Text.Json.Serialization;

namespace HtmlCssToImage.Net
{
    internal record ErrorResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; init; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; init; }

        [JsonPropertyName("message")]
        public string Message { get; init; }
    }
}