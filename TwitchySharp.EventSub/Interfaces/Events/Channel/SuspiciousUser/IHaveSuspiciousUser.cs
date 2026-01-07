using TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.SuspiciousUser;

/// <summary>
/// A suspicious chat user.
/// </summary>
public interface IHaveSuspiciousUser : IHaveUser
{
    /// <summary>
    /// The current status of the suspicious user as set by a moderator.
    /// </summary>
    SuspiciousUserStatus LowTrustStatus { get; }
}
