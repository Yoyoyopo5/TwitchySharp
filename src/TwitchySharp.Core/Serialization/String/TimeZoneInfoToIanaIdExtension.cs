using System;
using System.Text.Json;

namespace TwitchySharp.Serialization;

/// <summary>
/// Contains an extension method for converting a <see cref="TimeZoneInfo"/> to its respective IANA timezone id.
/// </summary>
public static class TimeZoneInfoToIanaIdExtension
{
    /// <summary>
    /// Convert a <see cref="TimeZoneInfo"/> to its corresponding IANA id.
    /// </summary>
    /// <param name="tzi">The time zone to get an IANA id of.</param>
    /// <returns>The <paramref name="tzi"/> IANA id.</returns>
    /// <exception cref="NotSupportedException"/>
    public static string ToIanaId(this TimeZoneInfo tzi)
        => TimeZoneInfo.TryConvertWindowsIdToIanaId(tzi.Id, out string? ianaId) switch
        {
            true => ianaId,
            false => tzi.HasIanaId switch
            {
                true => tzi.Id,
                false => throw new NotSupportedException($"Cannot convert timezone id '{tzi.Id}' to IANA id format.")
            }
        };
}
