using System.Linq;
using System.Text.Json.Serialization;

namespace HtmlCssToImage.Net
{
    public record CreateImageResponse
    {
        [JsonPropertyName("url")]
        public string Url { get; }
        
        public string Id { get; }

        public CreateImageResponse(string url)
        {
            Url = url;
            Id = Url.Split("/").Last();
        }
    }
}