using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.Analytics.Requests;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Analytics.Enums;

/// <summary>
/// Contains static definitions for report types for use with <see cref="GetExtensionAnalyticsRequest"/>.
/// </summary>
/// <param name="Value">The string value of the report type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionAnalyticsReportType, string>))]
public record ExtensionAnalyticsReportType(string Value) : ValueBackedEnum<string>(Value)
{
    public static ExtensionAnalyticsReportType OverviewV2 { get; } = new ExtensionAnalyticsReportType("overview_v2");
}
