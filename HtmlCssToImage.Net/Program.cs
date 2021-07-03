using System;
using System.Threading.Tasks;

namespace HtmlCssToImage.Net
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            HtmlCssToImageClient client = new HtmlCssToImageClient(
                new HtmlCssToImageCredentials(
                    "userid",
                    "apikey"));

            string html = "<blockquote class=\"twitter-tweet\" style=\"width: 400px;\" data-dnt=\"true\">\r\n<p lang=\"en\" dir=\"ltr\"></p>\r\n\r\n<a href=\"https://twitter.com/naftalibennett/status/1411408167263735814\"></a>\r\n\r\n</blockquote> <script async src=\"https://platform.twitter.com/widgets.js\" charset=\"utf-8\"></script>\r\n";

            Console.WriteLine(await client.CreateImageAsync(new CreateImageParameters(html)
            {
                DeviceScale = 3,
                CssSelector = ".twitter-tweet",
                Delay = TimeSpan.FromMilliseconds(1500)
            }));
        }        
    }
}