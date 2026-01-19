using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Analytics;

/// <summary>
/// Contains analytics information about a specific game category on Twitch.
/// </summary>
public record GameAnalyticsData
{
    /// <summary>
    /// An ID that identifies the game that the report was generated for.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The URL that you use to download the report. The URL is valid for 5 minutes.
    /// </summary>
    [JsonPropertyName("URL")]
    public required string Url { get; init; }
    /// <summary>
    /// The type of report.
    /// </summary>
    public required string Type { get; init; }
    /// <summary>
    /// The reporting window’s start and end dates.
    /// </summary>
    public required DateTimeOffsetRange DateRange { get; init; }
}
