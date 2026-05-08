using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Ads;

/// <summary>
/// Represents the status of a start commerical request.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-commercial">Start Commercial</see> for more information.
/// </summary>
public record StartedCommerical
{
    /// <summary>
    /// The length of the commercial you requested.
    /// If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds. 
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Length { get; init; }
    /// <summary>
    /// A message that indicates whether Twitch was able to serve an ad (typically empty?).
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// The number of seconds you must wait before running another commercial.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan RetryAfter { get; init; }
}
