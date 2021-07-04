using System.Linq;
using System.Text.Json.Serialization;

namespace HtmlCssToImage.Net
{
    public record CreateImageResponse
    {
        /// <summary>
        /// The created image's url.
        /// This url has no extension, meaning this image will be returned as png.
        /// </summary>
        /// <seealso cref="GetImageParameters"/>
        [JsonPropertyName("url")]
        public string Url { get; }
        
        /// <summary>
        /// The created image's id.
        /// </summary>
        public string Id { get; }

        public CreateImageResponse(string url)
        {
            Url = url;
            Id = Url.Split("/").Last();
        }
    }
}