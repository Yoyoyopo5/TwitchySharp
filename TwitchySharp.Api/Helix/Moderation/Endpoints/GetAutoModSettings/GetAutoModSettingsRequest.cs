using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster's AutoMod settings.
/// </summary>
/// <remarks>
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster's chat room.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-automod-settings">Get AutoMod Settings</see> for more information.
/// </remarks>
public record GetAutoModSettingsRequest
    : TwitchHelixRequest<GetAutoModSettingsResponse>
{
    protected override string Path => "/moderation/automod/settings";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorReadAutomodSettings)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get AutoMod settings for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }
}
