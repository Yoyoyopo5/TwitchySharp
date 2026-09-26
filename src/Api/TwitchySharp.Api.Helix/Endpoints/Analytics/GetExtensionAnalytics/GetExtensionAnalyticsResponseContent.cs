namespace TwitchySharp.Api.Helix.Analytics;
/// <summary>
/// Contains data about extension analytics.
/// </summary>
public record GetExtensionAnalyticsResponseContent
    : IPageableResponse
{
    /// <summary>
    /// A list of reports.
    /// </summary>
    /// <remarks>
    /// The reports are returned in no particular order; 
    /// however, the data within each report is in ascending order by date (newest first). 
    /// The report contains one row of data per day of the reporting window; 
    /// the report contains rows for only those days that the extension was used. 
    /// The array is empty if there are no reports.
    /// </remarks>
    public required ExtensionAnalyticsData[] Data { get; init; }
    public Pagination Pagination { get; init; }
}
