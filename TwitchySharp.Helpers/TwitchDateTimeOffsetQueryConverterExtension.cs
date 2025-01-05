using System;

namespace TwitchySharp.Helpers;

public static class TwitchDateTimeOffsetQueryConverterExtension
{
    public static string ToUniversalTwitchQueryString(this DateTimeOffset dateTimeOffset)
        => dateTimeOffset.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
}