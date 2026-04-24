using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Games;

/// <summary>
/// A game id from <see href="https://www.igdb.com/">IGDB</see>.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct IgdbId(string Value);
