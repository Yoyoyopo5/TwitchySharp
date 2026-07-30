namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// Contains data about the started ad.
/// </summary>
public record StartCommercialResponse
{
    /// <summary>
    /// An array that contains a single <see cref="StartedCommerical"/> with the status of your start commercial request.
    /// </summary>
    public required StartedCommerical[] Data { get; init; }
}
