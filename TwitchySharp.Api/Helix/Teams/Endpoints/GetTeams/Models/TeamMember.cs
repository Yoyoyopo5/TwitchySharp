using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Teams;

public record TeamMember
{
    public required UserId UserId { get; init; }
    public required UserLogin UserLogin { get; init; }
    public required UserName UserName { get; init; }
}
