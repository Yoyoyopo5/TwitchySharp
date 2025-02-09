using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Gets information about the specified Twitch team.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-teams">get teams</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="query">
/// The query to search for teams.
/// Use an instance of <see cref="TeamsQueryById"/> or <see cref="TeamsQueryByName"/> depending on how you want to find the team.
/// </param>
public class GetTeamsRequest(
    string clientId,
    string accessToken,
    TeamsQuery query
    )
    : HelixApiRequest<GetTeamsResponse>(
        "/teams" +
        new HttpQueryParameters()
            .Add("name", query.Name)
            .Add("id", query.Id),
        clientId,
        accessToken
        );

/// <summary>
/// Use derived classes <see cref="TeamsQueryById"/> and <see cref="TeamsQueryByName"/> to create a teams query.
/// </summary>
public record TeamsQuery
{
    /// <summary>
    /// The name of the team to get. 
    /// </summary>
    public string? Name { get; protected set; }
    /// <summary>
    /// The id of the team to get.
    /// </summary>
    public string? Id { get; protected set; }
    protected TeamsQuery() { }
}

/// <summary>
/// Query for a team by team name.
/// </summary>
public record TeamsQueryByName
    : TeamsQuery
{
    /// <summary>
    /// <inheritdoc cref="TeamsQueryByName"/>
    /// </summary>
    /// <param name="name">
    /// <inheritdoc cref="TeamsQuery" path="/summary"/>
    /// </param>
    public TeamsQueryByName(string name)
        => Name = name;
}

/// <summary>
/// Query for a team by team id.
/// </summary>
public record TeamsQueryById
    : TeamsQuery
{
    /// <summary>
    /// <inheritdoc cref="TeamsQueryById"/>
    /// </summary>
    /// <param name="teamId">
    /// <inheritdoc cref="TeamsQuery.Id" path="/summary"/>
    /// </param>
    public TeamsQueryById(string teamId)
        => Id = teamId;
}
