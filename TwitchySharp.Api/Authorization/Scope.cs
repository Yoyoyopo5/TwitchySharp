using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Api.Authorization;
public partial record Scope
{
    private readonly string _scope;
    /// <summary>
    /// If possible, please use static <see cref="Scope"/> definitions provided by this class.
    /// You can use this constructor to create a <see cref="Scope"/> when a static definition is not provided.
    /// </summary>
    /// <param name="scope">The Twitch scope string (e.g. "bits:read")</param>
    public Scope(string scope) {  _scope = scope; }
    public override string ToString()
        => _scope;
}

internal static class ScopeExtensions
{
    public static string FormatScopes(this IEnumerable<Scope> scopes)
    {
        StringBuilder sb = new();
        foreach (Scope scope in scopes)
        {
            sb.Append(scope.ToString());
            sb.Append('+');
        }
        return sb.ToString().TrimEnd('+');
    }
}
