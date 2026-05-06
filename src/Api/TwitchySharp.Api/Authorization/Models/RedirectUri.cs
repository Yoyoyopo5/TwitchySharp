using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// A redirect URI used in authorization client URLs.
/// </summary>
/// <param name="Value">The value of the URI.</param>
[Wrapper<string>]
public readonly partial record struct RedirectUri(string Value);
