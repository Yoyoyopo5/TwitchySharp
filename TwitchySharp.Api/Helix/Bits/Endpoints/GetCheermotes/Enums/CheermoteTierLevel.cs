using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Contains static definitions for possible Cheermote tier level ids.
/// </summary>
/// <param name="Value">The value of the Cheermote tier level.</param>
[Wrapper<string>]
public readonly partial record struct CheermoteTierLevel(string Value)
{
    public static CheermoteTierLevel One { get; } = new("1");
    public static CheermoteTierLevel OneHundred { get; } = new("100");
    public static CheermoteTierLevel OneThousand { get; } = new("1000");
    public static CheermoteTierLevel FiveThousand { get; } = new("5000");
    public static CheermoteTierLevel TenThousand { get; } = new("10000");
    public static CheermoteTierLevel OneHundredThousand { get; } = new("100000");
}
