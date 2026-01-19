using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Analytics;

/// <summary>
/// Contains static definitions for report types for use with <see cref="GetGameAnalyticsRequest"/>.
/// </summary>
/// <param name="Value">The string value of the report type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<GameAnalyticsReportType, string>))]
public record GameAnalyticsReportType(string Value) : ValueBackedEnum<string>(Value)
{
    public static GameAnalyticsReportType OverviewV2 { get; } = new GameAnalyticsReportType("overview_v2");
}