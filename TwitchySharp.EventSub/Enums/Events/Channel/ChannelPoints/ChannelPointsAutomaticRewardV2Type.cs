using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;

/// <summary>
/// Contains static definitions for possible automatic (built-in) channel points reward types.
/// </summary>
/// <param name="Value">The string value of the reward type.</param>
[Wrapper<string>]
public readonly partial record struct ChannelPointsAutomaticRewardV2Type(string Value)
{
    public static ChannelPointsAutomaticRewardV2Type SingleMessageBypassSubMode { get; } = new("single_message_bypass_sub_mode");
    public static ChannelPointsAutomaticRewardV2Type SendHighlightedMessage { get; } = new("send_highlighted_message");
    public static ChannelPointsAutomaticRewardV2Type RandomSubEmoteUnlock { get; } = new("random_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardV2Type ChosenSubEmoteUnlock { get; } = new("chosen_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardV2Type ChosenModifiedSubEmoteUnlock { get; } = new("chosen_modified_sub_emote_unlock");
}
