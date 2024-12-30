using System;

namespace TwitchySharp.Api.Models;

/// <summary>
/// Contains information about current Twitch rate limit usage.
/// This data is reported once for each call to the Twitch API.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/guide/#twitch-rate-limits">Twitch Rate Limits</see> for more information.
/// </summary>
/// <param name="Limit">The maximum amount of points that can be in your bucket.</param>
/// <param name="Remaining">The number of points currently in your bucket (after the last request).</param>
/// <param name="Reset">The date and time when your bucket is reset to full.</param>
public readonly record struct TwitchRateLimitDetails(int Limit, int Remaining, DateTimeOffset Reset);
