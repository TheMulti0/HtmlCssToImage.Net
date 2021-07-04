using Microsoft.Extensions.Configuration;

namespace HtmlCssToImage.Net.Tests
{
    public class HtmlCssToImageTestBase
    {
        protected readonly IHtmlCssToImageClient Client;

        protected HtmlCssToImageTestBase()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<HtmlCssToImageTestBase>().Build();

            var credentials = new HtmlCssToImageCredentials(config["UserId"], config["ApiKey"]);

            Client = new HtmlCssToImageClient(credentials);
        }
    }
}