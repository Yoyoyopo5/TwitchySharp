namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Gets information about the specified Twitch team.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-teams">Get Teams</see> for more information.
/// </remarks>
public record GetTeamsRequest
    : TwitchHelixRequest<GetTeamsResponse>
{
    protected override string Path => "/teams";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("name", Query.Name)
            .Add("id", Query.Id);

    /// <summary>
    /// The query specifying which team to retrieve.
    /// </summary>
    /// <remarks>
    /// Use <see cref="TeamsQueryByName"/> or <see cref="TeamsQueryById"/>.
    /// </remarks>
    public required TeamsQuery Query { get; init; }
}

/// <summary>
/// Base type for teams query parameters.
/// </summary>
/// <remarks>
/// Use derived types <see cref="TeamsQueryByName"/> or <see cref="TeamsQueryById"/>.
/// </remarks>
public abstract record TeamsQuery
{
    internal string? Name { get; init; }
    internal TeamId? Id { get; init; }
}

/// <summary>
/// Query for a team by team name.
/// </summary>
public record TeamsQueryByName : TeamsQuery
{
    /// <summary>
    /// The name of the team to get.
    /// </summary>
    public new required string Name
    {
        get => base.Name!;
        init => base.Name = value;
    }
}

/// <summary>
/// Query for a team by team id.
/// </summary>
public record TeamsQueryById : TeamsQuery
{
    /// <summary>
    /// The id of the team to get.
    /// </summary>
    public new required TeamId Id
    {
        get => base.Id!.Value;
        init => base.Id = value;
    }
}
