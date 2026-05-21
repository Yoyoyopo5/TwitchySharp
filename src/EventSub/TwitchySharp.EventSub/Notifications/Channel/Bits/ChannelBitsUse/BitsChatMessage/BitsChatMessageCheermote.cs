namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific cheermote used in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageCheermote
{
    public required CheermotePrefix Prefix { get; init; }
    public required int Bits { get; init; }
    public required CheermoteTier Tier { get; init; }
}
