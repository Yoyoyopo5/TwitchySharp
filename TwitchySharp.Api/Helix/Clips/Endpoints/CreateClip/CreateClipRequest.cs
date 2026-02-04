using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Creates a clip from the broadcaster's stream.
/// </summary>
/// <remarks>
/// <para>
/// This API captures up to 90 seconds of the broadcaster's stream.
/// The 90 seconds spans the point in the stream from when you called the API.
/// For example, if you call the API at the 4:00 minute mark, the API captures from approximately the 3:35 mark to approximately the 4:05 minute mark.
/// Twitch tries its best to capture 90 seconds of the stream, but the actual length may be less.
/// This may occur if you begin capturing the clip near the beginning or end of the stream.
/// </para>
/// <para>
/// By default, Twitch publishes up to the last 30 seconds of the 90 seconds window and provides a default title for the clip.
/// To specify the title and the portion of the 90 seconds window that's used for the clip, use the URL in the response's <see cref="CreateClipResponse.EditUrl"/> property.
/// You can specify a clip that's from 5 seconds to 60 seconds in length. The URL is valid for up to 24 hours or until the clip is published, whichever comes first.
/// </para>
/// <para>
/// Creating a clip is an asynchronous process that can take a short amount of time to complete.
/// To determine whether the clip was successfully created, call Get Clips using the <see cref="CreateClipResponse.Id"/> that this request returned.
/// If Get Clips returns the clip, the clip was successfully created. If after 15 seconds Get Clips hasn't returned the clip, assume it failed.
/// </para>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ClipsEdit"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-clip">Create Clip</see> for more information.
/// </remarks>
public record CreateClipRequest
    : TwitchHelixRequest<CreateClipResponse>
{
    protected override string Path => "/clips";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => User;
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ClipsEdit);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("title", Title)
            .Add("duration", Duration?.TotalSeconds.ToString());

    /// <summary>
    /// The user to create the clip as.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserIdentity User { get; init; }

    /// <summary>
    /// The user id of the broadcaster (channel) to create a clip for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The title of the clip to create.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// The length of the clip to create.
    /// </summary>
    /// <remarks>
    /// Can range from 5 to 60 seconds, with a resolution of 100ms.
    /// Defaults to 30 seconds if left <see langword="null"/>.
    /// </remarks>
    public TimeSpan? Duration { get; init; }
}
