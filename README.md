> The library is still under development and is not complete yet.

# HTML/CSS to Image .NET

This is an unofficial API wrapper of [HTML/CSS to Image API]("https://htmlcsstoimage.com/) in .NET!

### Usage
```cs
var credentials = new HttpCssToImageCredentials(userId, apiKey);

var client = new HttpCssToImageClient(credentials);

var request = new CreateImageRequest("<div>Hello world!</div>") 
{
    DeviceScale = 2
};

var image = await client.CreateImageAsync(request);

Console.WriteLine(image.Id);