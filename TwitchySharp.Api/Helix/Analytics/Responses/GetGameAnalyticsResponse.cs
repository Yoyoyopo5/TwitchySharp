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
    [JsonInclude, JsonRequired]
    public GameAnalyticsData[] Data { get; private set; } = [];
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages to page through.
    /// </summary>
    [JsonInclude, JsonRequired]
    public Pagination Pagination { get; private set; } = new();
}

/// <summary>
/// Contains analytics information about a specific game category on Twitch.
/// </summary>
public record GameAnalyticsData
{
    /// <summary>
    /// An ID that identifies the game that the report was generated for.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string GameId { get; private set; } = string.Empty;
    /// <summary>
    /// The URL that you use to download the report. The URL is valid for 5 minutes.
    /// </summary>
    [JsonInclude, JsonRequired, JsonPropertyName("URL")]
    public string Url { get; private set; } = string.Empty;
    /// <summary>
    /// The type of report.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Type { get; private set; } = string.Empty;
    /// <summary>
    /// The reporting window’s start and end dates.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffsetRange DateRange { get; private set; }
}
