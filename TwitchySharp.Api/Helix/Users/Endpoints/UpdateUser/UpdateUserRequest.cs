using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Updates a user’s description.
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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">
    /// A user access token that includes <see cref="Scope.UserEdit"/>.
    /// The user that created the access token is the one who will be updated.
    /// </param>
    /// <param name="description">
    /// The string to update the channel’s description to. 
    /// The description is limited to a maximum of 300 characters.
    /// To remove the description, set this to <see cref="string.Empty"/>.
    /// </param>
    public UpdateUserRequest(
        string clientId,
        string accessToken,
        string? description
        ) : base(
            "/users",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("description", description)
            )
    {
        Method = HttpMethod.Put;
    }
}
