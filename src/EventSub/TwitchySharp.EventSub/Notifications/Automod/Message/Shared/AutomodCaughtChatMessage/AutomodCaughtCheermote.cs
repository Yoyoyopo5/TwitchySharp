namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific cheermote that triggered Automod.
/// </summary>
public record AutomodCaughtCheermote
{
    /// <summary>
    /// The prefix of the caught cheermote.
    /// </summary>
    public required CheermotePrefix Prefix { get; init; }
    /// <summary>
    /// The amount of bits cheered.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The tier level of the cheermote.
    /// </summary>
    public required CheermoteTier Tier { get; init; }
}
