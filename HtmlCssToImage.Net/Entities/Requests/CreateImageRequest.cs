using System;
using System.Text.Json.Serialization;

namespace HtmlCssToImage.Net
{
    public record CreateImageRequest
    {
        /// <summary>
        /// This is the HTML you want to render. You can send an HTML snippet (<div>Your content</div>) or an entire webpage.
        /// </summary>
        /// <see cref="https://docs.htmlcsstoimage.com/guides/using-google-fonts/"/>
        [JsonPropertyName("html")]
        public string Html { get; }

        /// <summary>
        /// The fully qualified URL to a public webpage.
        /// Such as https://htmlcsstoimage.com.
        /// When passed this will override the html param and will generate a screenshot of the url.
        /// </summary>
        [JsonPropertyName("url")]
        public Uri Url { get; }
        
        /// <summary>
        /// The CSS for your image. When using with url it will be injected into the page.
        /// </summary>
        [JsonPropertyName("css")]
        public string Css { get; init; }
        
        /// <summary>
        /// Google fonts to be loaded.
        /// Example: Roboto.
        /// </summary>
        [JsonIgnore]
        public string[] GoogleFonts { get; init; } = Array.Empty<string>();

        [JsonPropertyName("google_fonts")]
        public string GoogleFontsToBeSerialized => string.Join('|', GoogleFonts);
        
        /// <summary>
        /// A CSS selector for an element on the webpage.
        /// We’ll crop the image to this specific element.
        /// For example: section#complete-toolkit.container-lg
        /// </summary>
        [JsonPropertyName("selector")]
        public string CssSelector { get; init; }
        
        /// <summary>
        /// The time the API should delay before generating the image.
        /// This is useful when waiting for JavaScript.
        /// We recommend starting with 500ms.
        /// Large values slow down the initial render time.
        /// </summary>
        [JsonIgnore]
        public TimeSpan Delay { get; init; } = TimeSpan.FromMilliseconds(500);
        
        [JsonPropertyName("ms_delay")]
        public int DelayMilliseconds => (int) Delay.TotalMilliseconds;
        
        /// <summary>
        /// This adjusts the pixel ratio for the screenshot.
        /// Minimum: 1, Maximum: 3.
        /// </summary>
        [JsonPropertyName("device_scale")]
        public double DeviceScale { get; init; } = 2;

        /// <summary>
        /// Set to true to control when the image is generated.
        /// Call ScreenshotReady() from JavaScript to generate the image.
        /// </summary>
        /// <see cref="https://docs.htmlcsstoimage.com/guides/render-when-ready/"/>
        [JsonPropertyName("render_when_ready")]
        public bool RenderWhenReady { get; init; }
        
        /// <summary>
        /// When set to true, the API will generate an image of the entire height of the page.
        /// </summary>
        [JsonPropertyName("full_screen")]
        public bool FullScreen { get; init; }
        
        /// <summary>
        /// Set the width of Chrome’s viewport. This will disable automatic cropping.
        /// Both height and width parameters must be set if using either.
        /// </summary>
        [JsonPropertyName("viewport_width")]
        public int ViewportWidth { get; init; }
        
        /// <summary>
        /// Set the height of Chrome’s viewport. This will disable automatic cropping.
        /// Both height and width parameters must be set if using either.
        /// </summary>
        [JsonPropertyName("viewport_height")]
        public int ViewportHeight { get; init; }

        public CreateImageRequest(string html)
        {
            Html = html;
        }

        public CreateImageRequest(Uri url)
        {
            Url = url;
        }
    }
}