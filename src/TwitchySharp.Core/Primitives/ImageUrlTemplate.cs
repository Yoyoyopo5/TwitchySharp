using System;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Helper class to create valid urls to game (category) cover images and stream thumbnails.
/// </summary>
/// <param name="Value"></param>
[Wrapper<string>]
public partial record ImageUrlTemplate(string Value)
{
    protected string WidthTemplate { get; set; } = "{width}";
    protected string HeightTemplate { get; set; } = "{height}";

    /// <summary>
    /// Creates a valid url to an image based on the requested width and height.
    /// </summary>
    /// <param name="width">The width of the image to get, in pixels.</param>
    /// <param name="height">The height of the image to get, in pixels.</param>
    /// <returns>A url to an image of the specified size.</returns>
    public Uri ToImageUrl(uint width, uint height)
        => new(Value
            .Replace(WidthTemplate, width.ToString())
            .Replace(HeightTemplate, height.ToString()));
}
