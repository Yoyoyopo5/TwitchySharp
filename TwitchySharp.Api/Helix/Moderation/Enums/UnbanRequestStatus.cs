namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Possible statuses of an unban request.
/// </summary>
public enum UnbanRequestStatus
{
    Pending,
    Approved,
    Denied,
    Acknowledged,
    Canceled
}
