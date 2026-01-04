using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a specific bits cheer in a chat message.
/// </summary>
public record ChannelChatMessageCheermote : IChatMessageCheermote
{
    /// <summary>
    /// The name portion of the Cheermote string that you use in chat to cheer Bits. 
    /// The full Cheermote string is the concatenation of {prefix} + {number of Bits}.
    /// </summary>
    /// <remarks>
    /// For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. 
    /// When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.
    /// </remarks>
    public required string Prefix { get; init; }
    /// <summary>
    /// The amount of bits cheered.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The tier level of the cheermote.
    /// </summary>
    public required int Tier { get; init; }
}
