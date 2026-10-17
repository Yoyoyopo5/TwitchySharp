namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// A cheermote in a pinned message.
/// </summary>
public record PinnedMessageCheermote
{
    /// <summary>
    /// The cheermote prefix.
    /// </summary>
    public CheermotePrefix Prefix { get; init; }
    /// <summary>
    /// The number of bits cheered.
    /// </summary>
    public int Bits { get; init; }
    /// <summary>
    /// The cheermote tier.
    /// </summary>
    // Other cheermote tier fields use a string type, not sure why this is integer.
    public PinnedMessageCheermoteTier Tier { get; init; }
}
