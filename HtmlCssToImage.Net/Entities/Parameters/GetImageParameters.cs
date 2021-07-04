
namespace HtmlCssToImage.Net
{
    public record GetImageParameters
    {
        /// <summary>
        /// Desired image's id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The width of the image.
        /// Maximum 5000.
        /// </summary>
        public int? Width { get; init; }

        /// <summary>
        /// The height of the image.
        /// Maximum 5000.
        /// </summary>
        public int? Height { get; init; }

        /// <summary>
        /// The image's format.
        /// The api returns png by default.
        /// </summary>
        public ImageFormat Format { get; init; }

        public GetImageParameters(string id)
        {
            Id = id;
        }
    }
}