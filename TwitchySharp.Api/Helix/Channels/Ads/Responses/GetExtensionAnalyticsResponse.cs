using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Channels.Ads.Responses;
public record GetExtensionAnalyticsResponse
{
    /// <summary>
    /// A list of reports. 
    /// The reports are returned in no particular order; 
    /// however, the data within each report is in ascending order by date (newest first). 
    /// The report contains one row of data per day of the reporting window; 
    /// the report contains rows for only those days that the extension was used. 
    /// The array is empty if there are no reports.
    /// </summary>
    [JsonInclude, JsonRequired]
    public ExtensionAnalyticsData[] Data { get; private set; } = [];
    [JsonInclude, JsonRequired]
    public Pagination Pagination { get; private set; } = new();

}

/// <summary>
/// Contains information about an extension's analytics, including a url used to download the report.
/// </summary>
public record ExtensionAnalyticsData
{
    /// <summary>
    /// An ID that identifies the extension that the report was generated for.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string ExtensionId { get; private set; } = string.Empty;
    /// <summary>
    /// The URL that you use to download the report. The URL is valid for 5 minutes.
    /// </summary>
    [JsonInclude, JsonRequired, JsonPropertyName("URL")]
    public string Url { get; private set; } = string.Empty;
    /// <summary>
    /// The type of report.
    /// </summary>
    public string Type { get; private set; } = string.Empty;
    /// <summary>
    /// The reporting window’s start and end dates.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateRange DateRange { get; private set; }
}