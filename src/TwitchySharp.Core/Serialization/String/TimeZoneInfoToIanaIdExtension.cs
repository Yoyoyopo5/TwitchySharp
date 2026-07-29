using System;
using System.Text.Json;

namespace TwitchySharp.Serialization;

public static class TimeZoneInfoToIanaIdExtension
{
    public static string ToIanaId(this TimeZoneInfo tzi)
        => TimeZoneInfo.TryConvertWindowsIdToIanaId(tzi.Id, out string? ianaId) switch
        {
            true => ianaId,
            false => tzi.HasIanaId switch
            {
                true => tzi.Id,
                false => throw new JsonException($"Cannot convert timezone id '{tzi.Id}' to IANA id format.")
            }
        };
}
