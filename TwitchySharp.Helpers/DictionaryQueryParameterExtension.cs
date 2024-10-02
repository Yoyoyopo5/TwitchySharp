using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Helpers;
/// <summary>
/// Helper method for formatting string key value pairs into HTTP query strings.
/// </summary>
public static class DictionaryQueryParameterExtension
{
    /// <summary>
    /// Creates an http query string that can be appended to a URI from a collection of string key-value pairs.
    /// </summary>
    /// <param name="queryParameters">The query parameters to encode.</param>
    /// <returns>A string that can be safely appended to an HTTP request URI.</returns>
    public static string ToHttpQueryString(this Dictionary<string, string?> queryParameters)
    {
        if (queryParameters.Count == 0)
            return string.Empty;
        StringBuilder sb = new('?');
        foreach (KeyValuePair<string, string?> parameter in queryParameters)
        {
            if (string.IsNullOrEmpty(parameter.Value))
                continue;
            sb.Append(parameter.Key);
            sb.Append('=');
            sb.Append(parameter.Value);
            sb.Append("&");
        }
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
}
