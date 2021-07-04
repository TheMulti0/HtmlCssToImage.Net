using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlCssToImage.Net.Tests
{
    [TestClass]
    public class GetImageTests : HtmlCssToImageTestBase
    {
        [TestMethod]
        public async Task TestArguments()
        {
            GetImageParameters[] parameters = {
                new(null),
                new("123456-abcdef")
                {
                    Width = 0
                },
                new("123456-abcdef")
                {
                    Width = 5001
                },
                new("123456-abcdef")
                {
                    Height = 0
                },
                new("123456-abcdef")
                {
                    Height = 5001
                }
            };

            foreach (var parameter in parameters)
            {
                await Assert.ThrowsExceptionAsync<ArgumentException>(
                    () => Client.GetImageAsync(parameter));
            }
        }
    }
}