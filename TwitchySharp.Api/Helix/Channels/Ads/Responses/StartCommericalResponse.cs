using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Channels.Ads.Responses;
/// <summary>
/// Contains data about the started ad.
/// </summary>
public class StartCommericalResponse
{
    /// <summary>
    /// An array that contains a single <see cref="StartedCommerical"/> with the status of your start commercial request.
    /// </summary>
    [JsonRequired]
    public StartedCommerical[] Data { get; } = [];
}

/// <summary>
/// Represents the status of a start commerical request.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-commercial">start commercial</see> for more information.
/// </summary>
public class StartedCommerical
{
    /// <summary>
    /// The length of the commercial you requested.
    /// If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds. 
    /// </summary>
    [JsonRequired]
    public int Length { get; } = 0;
    /// <summary>
    /// A message that indicates whether Twitch was able to serve an ad (typically empty?).
    /// </summary>
    [JsonRequired]
    public string Message { get; } = string.Empty;
    /// <summary>
    /// The number of seconds you must wait before running another commercial.
    /// </summary>
    [JsonRequired]
    public int RetryAfter { get; } = 0;
}
