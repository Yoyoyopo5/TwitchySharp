using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Analytics;
/// <summary>
/// Contains data about game analytics.
/// </summary>
public record GetGameAnalyticsResponse
{
    /// <summary>
    /// A list of reports. The reports are returned in no particular order; 
    /// however, the data within each report is in ascending order by date (newest first). 
    /// The report contains one row of data per day of the reporting window; 
    /// the report contains rows for only those days that the game was used. 
    /// A report is available only if the game was broadcast for at least 5 hours over the reporting period. 
    /// The array is empty if there are no reports.
    /// </summary>
    public required GameAnalyticsData[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}

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
