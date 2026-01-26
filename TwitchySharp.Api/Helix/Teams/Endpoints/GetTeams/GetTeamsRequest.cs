using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
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
    protected override string Path => "/teams";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("name", Name)
            .Add("id", Id);

    /// <summary>
    /// The name of the team to get.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="Id"/>. One of <see cref="Name"/> or <see cref="Id"/> must be set.
    /// </remarks>
    public string? Name { get; set; }
    /// <summary>
    /// The id of the team to get.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="Name"/>. One of <see cref="Name"/> or <see cref="Id"/> must be set.
    /// </remarks>
    public TeamId? Id { get; set; }
}
