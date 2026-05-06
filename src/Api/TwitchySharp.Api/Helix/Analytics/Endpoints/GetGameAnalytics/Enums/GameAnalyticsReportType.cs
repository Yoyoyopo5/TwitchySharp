using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Analytics;

/// <summary>
/// Contains static definitions for report types for use with <see cref="GetGameAnalyticsRequest"/>.
/// </summary>
/// <param name="Value">The string value of the report type.</param>
[Wrapper<string>]
public readonly partial record struct GameAnalyticsReportType(string Value)
{
    public static GameAnalyticsReportType OverviewV2 { get; } = new GameAnalyticsReportType("overview_v2");
}