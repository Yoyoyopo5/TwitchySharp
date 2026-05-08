using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api;

/// <summary>
/// Represents the amount of results per page to fetch.
/// </summary>
/// <param name="Value">The integer value of the amount.</param>
[Wrapper<int>]
public readonly partial record struct PaginationAmount(int Value);
