using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Analytics;
/// <summary>
/// Contains data about extension analytics.
/// </summary>
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
    public required ExtensionAnalyticsData[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages to page through.
    /// </summary>
    public Pagination? Pagination { get; init; }
}