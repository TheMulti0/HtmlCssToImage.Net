using System;

namespace HtmlCssToImage.Net
{
    public class HtmlCssToImageException : Exception
    {
        /// <summary>
        /// Error title
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Http response status code
        /// </summary>
        public int StatusCode { get; }

        internal HtmlCssToImageException(ErrorResponse response) : base(response.Message)
        {
            Error = response.Error;
            StatusCode = response.StatusCode;
        }
    }
}