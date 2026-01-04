namespace TwitchySharp.EventSub.Interfaces;

/// <summary>
/// A cheermote appearing in a Twitch chat message.
/// </summary>
public interface IChatMessageCheermote
{
    /// <summary>
    /// The name portion of the Cheermote string that you use in chat to cheer Bits. 
    /// </summary>
    /// <remarks>
    /// The full Cheermote string is the concatenation of {prefix} + {number of Bits}. 
    /// For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. 
    /// When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.
    /// </remarks>
    string Prefix { get; }
    /// <summary>
    /// The amount of Bits cheered.
    /// </summary>
    int Bits { get; }
    /// <summary>
    /// The tier level of the cheermote.
    /// </summary>
    int Tier { get; }
}
