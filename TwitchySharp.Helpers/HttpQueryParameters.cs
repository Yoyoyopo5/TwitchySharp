using System;
using System.Collections.Generic;
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
    {
        if (_parameters.Count == 0)
            return string.Empty;
        StringBuilder sb = new("?");
        foreach (KeyValuePair<string, string> parameter in _parameters)
        {
            if (string.IsNullOrEmpty(parameter.Value))
                continue;
            sb.Append(parameter.Key);
            sb.Append('=');
            sb.Append(parameter.Value);
            sb.Append("&");
        }
        sb.Remove(sb.Length - 1, 1); // Remove final '&'
        return sb.ToString();
    }
}
