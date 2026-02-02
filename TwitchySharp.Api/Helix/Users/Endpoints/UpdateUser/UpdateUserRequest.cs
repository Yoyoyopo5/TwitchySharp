using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Updates a user's description.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserEdit"/>.
/// To include the user's email address in the response, the token must also include <see cref="Scope.UserReadEmail"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user">Update User</see> for more information.
/// </remarks>
public record UpdateUserRequest
    : TwitchHelixRequest<UpdateUserResponse>
{
    protected override string Path => "/users";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchApiIdentity DefaultIdentity => User;
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.UserEdit);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("description", Description);

    /// <summary>
    /// The user to update.
    /// </summary>
    public required UserIdentity User { get; init; }

    /// <summary>
    /// The string to update the channel's description to.
    /// </summary>
    /// <remarks>
    /// The description is limited to a maximum of 300 characters.
    /// To remove the description, set this to <see cref="string.Empty"/>.
    /// </remarks>
    public string? Description { get; init; }
}
