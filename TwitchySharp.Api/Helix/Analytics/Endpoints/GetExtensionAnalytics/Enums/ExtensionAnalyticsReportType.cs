using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Analytics;

/// <summary>
/// Contains static definitions for report types for use with <see cref="GetExtensionAnalyticsRequest"/>.
/// </summary>
/// <param name="Value">The string value of the report type.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionAnalyticsReportType(string Value)
{
    public static ExtensionAnalyticsReportType OverviewV2 { get; } = new ExtensionAnalyticsReportType("overview_v2");
}
