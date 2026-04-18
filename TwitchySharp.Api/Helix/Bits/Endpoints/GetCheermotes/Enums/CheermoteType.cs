using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Contains static definitions for possible Cheermote types.
/// </summary>
/// <param name="Value">The string value of the Cheermote type.</param>
[Wrapper<string>]
public readonly partial record struct CheermoteType(string Value)
{
    /// <summary>
    /// A Twitch-defined Cheermote that is shown in the Bits card.
    /// </summary>
    public static CheermoteType GlobalFirstParty { get; } = new("global_first_party");
    /// <summary>
    /// A Twitch-defined Cheermote that is not shown in the Bits card.
    /// </summary>
    public static CheermoteType GlobalThirdParty { get; } = new("global_third_party");
    /// <summary>
    /// A broadcaster-defined Cheermote.
    /// </summary>
    public static CheermoteType ChannelCustom { get; } = new("channel_custom");
    /// <summary>
    /// Do not use; for internal use only.
    /// </summary>
    public static CheermoteType DisplayOnly { get; } = new("display_only");
    /// <summary>
    /// A sponsor-defined Cheermote. 
    /// When used, the sponsor adds additional Bits to the amount that the user cheered. 
    /// For example, if the user cheered Terminator100, the broadcaster might receive 110 Bits, which includes the sponsor's 10 Bits contribution.
    /// </summary>
    public static CheermoteType Sponsored { get; } = new("sponsored");
}
