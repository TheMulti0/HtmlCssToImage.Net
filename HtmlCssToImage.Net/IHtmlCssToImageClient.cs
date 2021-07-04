using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlCssToImage.Net
{
    public interface IHtmlCssToImageClient
    {
        /// <summary>
        /// Used to generate an image.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="HtmlCssToImageException">Thrown when api response is an error</exception>
        /// <returns></returns>
        Task<CreateImageResponse> CreateImageAsync(
            CreateImageParameters parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// After creating an image, use the returned id to download your image.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="HtmlCssToImageException">Thrown when api response is an error</exception>
        /// <returns></returns>
        Task<Stream> GetImageAsync(
            GetImageParameters parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Used to delete an image using the API request to your image URL.
        /// This will remove your image from HtmlCssToImage's servers and clear the caching for the image in our CDN.
        /// All data and copies of the image are deleted. This cannot be undone.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="HtmlCssToImageException">Thrown when api response is an error</exception>
        /// <returns></returns>
        Task DeleteImageAsync(
            string id,
            CancellationToken cancellationToken = default);
    }
}