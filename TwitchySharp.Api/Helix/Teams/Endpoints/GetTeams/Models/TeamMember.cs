namespace TwitchySharp.Api.Helix.Teams;

public record TeamMember
{
    public required string UserId { get; init; }
    public required string UserLogin { get; init; }
    public required string UserName { get; init; }
}
