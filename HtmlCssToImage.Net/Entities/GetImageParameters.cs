
namespace HtmlCssToImage.Net
{
    public record GetImageParameters
    {
        public string Id { get; }

        public int? Width { get; init; }

        public int? Height { get; init; }

        public ImageFormat Format { get; init; }

        public GetImageParameters(string id)
        {
            Id = id;
        }
    }
}