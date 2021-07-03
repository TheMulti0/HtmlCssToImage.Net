using System.Text.Json.Serialization;

namespace HtmlCssToImage.Net
{
    internal record CreateImageResponse
    {
        [JsonPropertyName("url")]
        public string Url { get; init; }
    }
}