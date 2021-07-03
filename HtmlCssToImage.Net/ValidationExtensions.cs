using System;

namespace HtmlCssToImage.Net
{
    internal static class ValidationExtensions
    {
        public static void Validate(this CreateImageParameters p)
        {
            if (p.Html == null && p.Url == null)
            {
                throw new ArgumentException("Html or Url must be set");
            }
            
            if (p.DeviceScale < 1 || p.DeviceScale > 3)
            {
                throw new ArgumentException("Device scale must be between 1 to 3");
            }
        }
    }
}