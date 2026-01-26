using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets the broadcaster's list editors.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelReadEditors"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-editors">Get Channel Editors</see> for more information.
/// </remarks>
public record GetChannelEditorsRequest
    : TwitchHelixRequest<GetChannelEditorsResponse>
{
    protected override string Path => "/channels/editors";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadEditors ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster that owns the channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ChannelReadEditors"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}
