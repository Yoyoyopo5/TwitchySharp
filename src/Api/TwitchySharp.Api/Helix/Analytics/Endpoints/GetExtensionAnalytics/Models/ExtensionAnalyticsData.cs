using System;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Analytics;

/// <summary>
/// Contains information about an extension's analytics, including a url used to download the report.
/// </summary>
public record ExtensionAnalyticsData
{
    /// <summary>
    /// An ID that identifies the extension that the report was generated for.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }
    /// <summary>
    /// The URL that you use to download the report. The URL is valid for 5 minutes.
    /// </summary>
    [JsonPropertyName("URL")]
    public required Uri Url { get; init; }
    /// <summary>
    /// The type of report.
    /// </summary>
    public required ExtensionAnalyticsReportType Type { get; init; }
    /// <summary>
    /// The reporting window’s start and end dates.
    /// </summary>
    public required DateTimeOffsetRange DateRange { get; init; }
}
