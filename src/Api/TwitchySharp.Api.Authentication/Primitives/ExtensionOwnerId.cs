using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Authentication;

/// <summary>
/// The user id of the owner of a Twitch extension.
/// </summary>
/// <param name="Value">The <see cref="UserId"/> of the extension owner.</param>
[Wrapper<UserId>]
public readonly partial record struct ExtensionOwnerId(UserId Value);
