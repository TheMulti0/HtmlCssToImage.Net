using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlCssToImage.Net.Tests
{
    [TestClass]
    public class CreateImageTests : HtmlCssToImageTestBase
    {
        [TestMethod]
        public async Task TestSimpleHtml()
        {
            const string html = "<a>Test</a>";

            await Client.CreateImageAsync(new CreateImageParameters(html));
        }
        
        [TestMethod]
        public async Task TestHtmlWithGoogleFont()
        {
            const string html = "<a style=\"font-family: \'Festive\', cursive;\">Festive</a>";   

            await Client.CreateImageAsync(new CreateImageParameters(html)
            {
                GoogleFonts = new []{ "Festive" }
            });
        }
        
        [TestMethod]
        public async Task TestHtmlWithGoogleFonts()
        {
            const string html = "<a style=\"font-family: \'Festive\', cursive;\">Festive</a>\r\n<a style=\"font-family: \'Oswald\', sans-serif;\">Oswald</a>";   

            await Client.CreateImageAsync(new CreateImageParameters(html)
            {
                GoogleFonts = new []{ "Festive", "Oswald" }
            });
        }
        
        [TestMethod]
        public async Task TestUrl()
        {
            await Client.CreateImageAsync(new CreateImageParameters(new Uri("https://google.com")));
        }
        
        [TestMethod]
        public async Task TestArguments()
        {
            CreateImageParameters[] parameters = {
                new(null as Uri),
                new(null as string),
                new("https://google.com")
                {
                    DeviceScale = 0
                },
                new("https://google.com")
                {
                    DeviceScale = 4
                }
            };

            foreach (CreateImageParameters parameter in parameters)
            {
                await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => Client.CreateImageAsync(parameter));
            }
        }
    }
}