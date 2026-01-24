using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">
    /// The request parameters.
    /// Use an instance of <see cref="TeamsQueryById"/> or <see cref="TeamsQueryByName"/> depending on how you want to find the team.
    /// </param>
    public GetTeamsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetTeamsRequestParameters parameters
        ) : base(
            "/teams",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("name", parameters.Name)
                .Add("id", parameters.Id)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetTeamsRequest"/>.
/// </summary>
/// <remarks>
/// Use derived classes <see cref="TeamsQueryById"/> and <see cref="TeamsQueryByName"/> to create a teams query.
/// </remarks>
public record GetTeamsRequestParameters
{
    /// <summary>
    /// The name of the team to get. 
    /// </summary>
    public string? Name { get; protected set; }
    /// <summary>
    /// The id of the team to get.
    /// </summary>
    public TeamId? Id { get; protected set; }
    protected GetTeamsRequestParameters() { }
}

/// <summary>
/// Query for a team by team name.
/// </summary>
public record TeamsQueryByName
    : GetTeamsRequestParameters
{
    /// <summary>
    /// <inheritdoc cref="TeamsQueryByName"/>
    /// </summary>
    /// <param name="name">
    /// <inheritdoc cref="GetTeamsRequestParameters" path="/summary"/>
    /// </param>
    public TeamsQueryByName(string name)
        => Name = name;
}

/// <summary>
/// Query for a team by team id.
/// </summary>
public record TeamsQueryById
    : GetTeamsRequestParameters
{
    /// <summary>
    /// <inheritdoc cref="TeamsQueryById"/>
    /// </summary>
    /// <param name="teamId">
    /// <inheritdoc cref="GetTeamsRequestParameters.Id" path="/summary"/>
    /// </param>
    public TeamsQueryById(TeamId teamId)
        => Id = teamId;
}
