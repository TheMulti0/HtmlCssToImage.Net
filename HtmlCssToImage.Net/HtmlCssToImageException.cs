using System;

namespace HtmlCssToImage.Net
{
    public class HtmlCssToImageException : Exception
    {
        public string Error { get; }

        public int StatusCode { get; }

        internal HtmlCssToImageException(ErrorResponse response) : base(response.Message)
        {
            Error = response.Error;
            StatusCode = response.StatusCode;
        }
    }
}