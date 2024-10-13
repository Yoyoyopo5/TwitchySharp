using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Channels.Ads;
/// <summary>
/// Contains data about the started ad.
/// </summary>
public record StartCommericalResponse
{
    /// <summary>
    /// An array that contains a single <see cref="StartedCommerical"/> with the status of your start commercial request.
    /// </summary>
    public required StartedCommerical[] Data { get; init; }
}

/// <summary>
/// Represents the status of a start commerical request.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-commercial">start commercial</see> for more information.
/// </summary>
public record StartedCommerical
{
    /// <summary>
    /// The length of the commercial you requested.
    /// If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds. 
    /// </summary>
    public required int Length { get; init; }
    /// <summary>
    /// A message that indicates whether Twitch was able to serve an ad (typically empty?).
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// The number of seconds you must wait before running another commercial.
    /// </summary>
    public required int RetryAfter { get; init; }
}
