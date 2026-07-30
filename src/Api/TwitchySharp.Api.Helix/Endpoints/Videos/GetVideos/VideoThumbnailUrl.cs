using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// A template used to get image URLs for Twitch videos via <see cref="ToImageUrl(uint, uint)"/>.
/// Reccomended sizes are 320x180 and multiples.
/// </summary>
[Wrapper<string>]
public partial record VideoThumbnailUrl : ImageUrlTemplate
{
    public VideoThumbnailUrl(string Value) : base(Value)
        => (WidthTemplate, HeightTemplate) = ("%{width}", "%{height}");
}
