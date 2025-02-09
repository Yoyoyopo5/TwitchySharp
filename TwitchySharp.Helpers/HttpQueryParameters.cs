using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchySharp.Helpers;
/// <summary>
/// Helper class for creating HTTP query strings.
/// Allows for multiple parameters with the same name.
/// </summary>
public class HttpQueryParameters
{
    private List<KeyValuePair<string, string>> _parameters = [];

    public HttpQueryParameters Add(string key, string? value)
    {
        if (string.IsNullOrEmpty(value))
            return this;
        _parameters.Add(new KeyValuePair<string, string>(key, value!)); // dumb compiler
        return this;
    }

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

    public override string ToString()
        => _parameters switch
        {
            { Count: > 0 } parameters => "?" + 
                string.Join(
                    '&',
                    parameters
                        .Where(p => !string.IsNullOrEmpty(p.Value))
                        .Select(p => $"{p.Key}={p.Value}")),
            _ => string.Empty
        };
}
