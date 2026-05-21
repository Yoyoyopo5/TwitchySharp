using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.AddBlockedTerm"/>, <see cref="ChannelModerateActionType.AddPermittedTerm"/>, <see cref="ChannelModerateActionType.RemoveBlockedTerm"/>, or <see cref="ChannelModerateActionType.RemovePermittedTerm"/> action.
/// </summary>
public partial record ChannelModerateAutomodTermsAction
{
    /// <summary>
    /// Contains static definitions for possible Automod terms action types.
    /// </summary>
    /// <param name="Value">The string value of the action type.</param>
    [Wrapper<string>]
    public readonly partial record struct ActionType(string Value)
    {
        public static ActionType Add { get; } = new("add");
        public static ActionType Remove { get; } = new("remove");
    }
    /// <summary>
    /// Contains static definitions for possible Automod terms list types.
    /// </summary>
    /// <param name="Value">The string value of the list type.</param>
    [Wrapper<string>]
    public readonly partial record struct ListType(string Value)
    {
        public static ListType BlockedTerms { get; } = new("blocked");
        public static ListType PermittedTerms { get; } = new("permitted");
    }

    /// <summary>
    /// The Automod terms action that was performed.
    /// </summary>
    public required ActionType Action { get; init; }
    /// <summary>
    /// The Automod terms list that the action was performed on.
    /// </summary>
    public required ListType List { get; init; }
    /// <summary>
    /// The terms that were added or removed.
    /// </summary>
    public required string[] Terms { get; init; }
    /// <summary>
    /// Indicates whether the action was due to an Automod message approve or deny action.
    /// </summary>
    /// <remarks>
    /// Dev Note: I think this is refering to when Automod prompts moderators to respond to a specific flagged message.
    /// </remarks>
    public required bool FromAutomod { get; init; }
}
