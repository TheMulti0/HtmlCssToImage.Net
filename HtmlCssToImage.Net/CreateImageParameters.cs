using System;
using System.Text.Json.Serialization;

namespace HtmlCssToImage.Net
{
    public record CreateImageParameters
    {
        [JsonPropertyName("html")]
        public string Html { get; }

        [JsonPropertyName("url")]
        public Uri Url { get; }
        
        [JsonPropertyName("css")]
        public string Css { get; init; }
        
        [JsonIgnore]
        public string[] GoogleFonts { get; init; } = Array.Empty<string>();

        [JsonPropertyName("google_fonts")]
        public string GoogleFontsToBeSerialized => string.Join('|', GoogleFonts);
        
        [JsonPropertyName("selector")]
        public string CssSelector { get; init; }
        
        [JsonIgnore]
        public TimeSpan Delay { get; init; } = TimeSpan.FromMilliseconds(500);

        [JsonPropertyName("ms_delay")]
        public int DelayMilliseconds => (int) Delay.TotalMilliseconds;
        
        [JsonPropertyName("device_scale")]
        public double DeviceScale { get; init; } = 2;
        
        [JsonPropertyName("render_when_ready")]
        public bool RenderWhenReady { get; init; }
        
        [JsonPropertyName("full_screen")]
        public bool FullScreen { get; init; }
        
        [JsonPropertyName("viewport_width")]
        public int ViewportWidth { get; init; }
        
        [JsonPropertyName("viewport_height")]
        public int ViewportHeight { get; init; }

        public CreateImageParameters(string html)
        {
            Html = html;
        }

        public CreateImageParameters(Uri url)
        {
            Url = url;
        }
    }
}