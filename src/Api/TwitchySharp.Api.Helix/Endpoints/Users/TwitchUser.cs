namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a specific Twitch user.
/// </summary>
public record TwitchUser
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    public required UserId Id { get; init; }
    /// <summary>
    /// The login (username) of the user.
    /// This is what users use to log in. 
    /// Usernames use only lowercase ASCII characters, numbers, and underscores.
    /// </summary>
    public required UserLogin Login { get; init; }
    /// <summary>
    /// The display name of the user.
    /// This is how users display in chats and stream descriptions. 
    /// Display names can have capital letters and unicode symbols.
    /// </summary>
    public required UserName DisplayName { get; init; }
    /// <summary>
    /// The type of user.
    /// This is used to distinguish Twitch staff from normal users.
    /// </summary>
    public required TwitchUserType Type { get; init; }
    /// <summary>
    /// The user's broadcaster type.
    /// </summary>
    public required TwitchBroadcasterType BroadcasterType { get; init; }
    /// <summary>
    /// The user’s description of their channel.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// A URL to the user’s profile image.
    /// </summary>
    public required Uri ProfileImageUrl { get; init; }
    /// <summary>
    /// A URL to the user’s offline image.
    /// </summary>
    public required Uri OfflineImageUrl { get; init; }
    /// <summary>
    /// The user’s verified email address. 
    /// The object includes this field only if the user access token includes <see cref="Scope.UserReadEmail"/> and the token was created by this specific user.
    /// </summary>
    public UserEmail? Email { get; init; }
    /// <summary>
    /// The date and time the user's account was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
