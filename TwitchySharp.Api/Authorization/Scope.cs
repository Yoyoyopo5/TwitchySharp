using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Authorization;
public partial record Scope : ValueBackedEnum<string>
{
    /// <summary>
    /// If possible, please use static <see cref="Scope"/> definitions provided by this class.
    /// You can use this constructor to create a <see cref="Scope"/> when a static definition is not provided.
    /// </summary>
    /// <param name="scope">The Twitch scope string (e.g. "bits:read")</param>
    public Scope(string scope) : base(scope) { }
}

internal static class ScopeExtensions
{
    public static string FormatScopes(this IEnumerable<Scope> scopes)
    {
        StringBuilder sb = new();
        foreach (Scope scope in scopes)
        {
            sb.Append(scope);
            sb.Append('+');
        }
        return sb.ToString().TrimEnd('+');
    }
}
