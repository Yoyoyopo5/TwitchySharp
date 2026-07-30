using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api;

/// <summary>
/// A cursor used for pagination.
/// </summary>
/// <param name="Value">The cursor's string value.</param>
[Wrapper<string>]
public readonly partial record struct PaginationCursor(string Value);
