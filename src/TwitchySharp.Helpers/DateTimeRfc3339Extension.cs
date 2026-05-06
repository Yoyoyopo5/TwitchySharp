using System;

namespace TwitchySharp.Helpers;

public static class DateTimeRfc3339Extension
{
    /// <summary>
    /// Serializes a <see cref="DateTime"/> to an RFC 3339 formatted string accepted by the Twitch API.
    /// </summary>
    /// <param name="dateTime">The date and time to serialize.</param>
    /// <returns>A string representing the <paramref name="dateTime"/> in RFC 3339 format.</returns>
    public static string ToRfc3339(this DateTime dateTime)
        => dateTime.ToString("yyyy-MM-dd'T'HH:mm:ssK");
}