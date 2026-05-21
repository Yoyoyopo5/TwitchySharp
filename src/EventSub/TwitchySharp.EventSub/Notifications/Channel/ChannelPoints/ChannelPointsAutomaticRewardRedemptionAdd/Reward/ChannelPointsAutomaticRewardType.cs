using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible automatic (built-in) channel points reward types.
/// </summary>
/// <param name="Value">The string value of the reward type.</param>
[Wrapper<string>]
public readonly partial record struct ChannelPointsAutomaticRewardType(string Value)
{
    public static ChannelPointsAutomaticRewardType SingleMessageBypassSubMode { get; } = new("single_message_bypass_sub_mode");
    public static ChannelPointsAutomaticRewardType SendHighlightedMessage { get; } = new("send_highlighted_message");
    public static ChannelPointsAutomaticRewardType RandomSubEmoteUnlock { get; } = new("random_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardType ChosenSubEmoteUnlock { get; } = new("chosen_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardType ChosenModifiedSubEmoteUnlock { get; } = new("chosen_modified_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardType MessageEffect { get; } = new("message_effect");
    public static ChannelPointsAutomaticRewardType GigantifyAnEmote { get; } = new("gigantify_an_emote");
    public static ChannelPointsAutomaticRewardType Celebration { get; } = new("celebration");
}
