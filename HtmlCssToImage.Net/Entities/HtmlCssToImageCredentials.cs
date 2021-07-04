namespace HtmlCssToImage.Net
{
    /// <summary>
    /// The UserId and ApiKey required to use the api.
    /// Both of these available from the dashboard.
    /// </summary>
    /// <see cref="https://htmlcsstoimage.com/dashboard"/>
    public record HtmlCssToImageCredentials(string UserId, string ApiKey);
}