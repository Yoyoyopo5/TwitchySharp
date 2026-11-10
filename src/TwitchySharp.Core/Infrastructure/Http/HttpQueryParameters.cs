using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchySharp.Infrastructure.Http;
/// <summary>
/// Helper class for creating HTTP query strings.
/// Allows for multiple parameters with the same name.
/// </summary>
/// <remarks>
/// Note that this class is <i>mutable</i>.
/// </remarks>
public class HttpQueryParameters
{
    private readonly List<KeyValuePair<string, string?>> _parameters = [];

    /// <summary>
    /// Add a key and value to the query.
    /// </summary>
    /// <remarks>
    /// Does not overwrite previously added values with the same key.
    /// </remarks>
    /// <param name="key">The query parameter key.</param>
    /// <param name="value">The query parameter value for the <paramref name="key"/>.</param>
    /// <returns>This instance.</returns>
    public HttpQueryParameters Add(string key, string? value)
    {
        _parameters.Add(new KeyValuePair<string, string?>(key, value));
        return this;
    }

    /// <summary>
    /// Add multiple values for a key to the query.
    /// </summary>
    /// <param name="key">The query key to add values to.</param>
    /// <param name="values">The value to add for the <paramref name="key"/>.</param>
    /// <returns>This instance.</returns>
    public HttpQueryParameters Add(string key, IEnumerable<string?>? values)
    {
        if (values is null)
            return this;
        foreach (string? value in values)
        {
            Add(key, value);
        }
        return this;
    }

    /// <summary>
    /// Get the query string.
    /// </summary>
    /// <returns>The query string.</returns>
    public override string ToString()
        => ToStringEfficient();

    private string ToStringLinq()
        => _parameters switch
        {
            { Count: > 0 } parameters => "?" +
                string.Join(
                    '&',
                    parameters
                        .Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}")),
            _ => string.Empty
        };

    private string ToStringEfficient()
    {
        if (_parameters is null || _parameters.Count == 0)
            return string.Empty;

        StringBuilder sb = new("?");
        bool first = true;
        foreach ((string key, string? value) in _parameters)
        {
            if (string.IsNullOrEmpty(key) || value is null) // Don't write null.
                continue;
            if (!first)
                sb.Append('&');
            sb.Append(Uri.EscapeDataString(key));
            sb.Append('=');
            sb.Append(Uri.EscapeDataString(value ?? string.Empty));
            first = false;
        }
        return sb.Length > 1 ? sb.ToString() : string.Empty;
    }
}
