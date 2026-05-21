using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// The name portion of the Cheermote string that you use in chat to cheer Bits. 
/// The full Cheermote string is the concatenation of {prefix} + {number of Bits}.
/// </summary>
/// <remarks>
/// For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. 
/// When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.
/// </remarks>
/// <param name="Value">The string value of the prefix.</param>

[Wrapper<string>]
public readonly partial record struct CheermotePrefix(string Value);
