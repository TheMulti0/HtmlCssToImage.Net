using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlCssToImage.Net
{
    public interface IHtmlCssToImageClient
    {
        Task<CreateImageResponse> CreateImageAsync(
            CreateImageParameters parameters,
            CancellationToken cancellationToken);

        Task<Stream> GetImageAsync(
            GetImageParameters parameters,
            CancellationToken cancellationToken);
    }
}