using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a specific Twitch user.
/// </summary>
public record TwitchUser
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The login (username) of the user.
    /// This is what users use to log in. 
    /// Usernames use only lowercase ASCII characters, numbers, and underscores.
    /// </summary>
    public required string Login { get; init; }
    /// <summary>
    /// The display name of the user.
    /// This is how users display in chats and stream descriptions. 
    /// Display names can have capital letters and unicode symbols.
    /// </summary>
    public required string DisplayName { get; init; }
    /// <summary>
    /// The type of user.
    /// This is used to distinguish Twitch staff from normal users.
    /// </summary>
    [JsonConverter(typeof(ValueBackedEnumJsonConverter<TwitchUserType, string>))]
    public required TwitchUserType Type { get; init; }
    /// <summary>
    /// The user's broadcaster type.
    /// </summary>
    [JsonConverter(typeof(ValueBackedEnumJsonConverter<TwitchUserType, string>))]
    public required TwitchBroadcasterType BroadcasterType { get; init; }
    /// <summary>
    /// The user’s description of their channel.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// A URL to the user’s profile image.
    /// </summary>
    public required string ProfileImageUrl { get; init; }
    /// <summary>
    /// A URL to the user’s offline image.
    /// </summary>
    public required string OfflineImageUrl { get; init; }
    /// <summary>
    /// The user’s verified email address. 
    /// The object includes this field only if the user access token includes <see cref="Scope.UserReadEmail"/> and the token was created by this specific user.
    /// </summary>
    public string? Email { get; init; }
    /// <summary>
    /// The date and time the user's account was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
